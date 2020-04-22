using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Helpers.Cryptography;
using Helpers.Exceptions;
using Helpers.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Auth;
using Microsoft.Extensions.Configuration;
using WEBAPI.Enums;
using WEBAPI.ViewModels.Sms;

namespace WEBAPI.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IUsersService _usersService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;

        public IConfiguration Configuration { get; }

        public AuthService(ApplicationDatabaseContext context, IConfiguration configuration, IUsersService usersService, IMapper mapper, IEmailService emailService, ISmsService smsService)
        {
            _context = context;
            Configuration = configuration;
            _mapper = mapper;
            _emailService = emailService;
            _smsService = smsService;
        }

        public TokenViewModel TokenMobile(GetTokenViewModel model)
        {
            var user = AuthUser(model);
            return GetToken(user);
        }

        public TokenViewModel Token(GetTokenViewModel model, IPAddress ipAddress)
        {
            var user = AuthUser(model);
            if (user.Role.Name != "SuperAdmin")
            {
                if (!CheckIp(user.Id, ipAddress)) throw new Exception("confirmsms");
            } else if (user.Role.Name == "SuperAdmin")
            {
                TwoFactor(user.Id);
            }

            return GetToken(user);
        }

        public TokenViewModel Refresh(RefreshTokenViewModel model)
        {
            var refreshToken = _context.RefreshTokens.FirstOrDefault(x => x.Id.ToString() == model.RefreshToken);
            if (refreshToken == null) throw new RefreshTokenNotFoundException("refreshexpired");

            if (refreshToken.ValidTo < DateTime.UtcNow) throw new RefreshTokenExpiredException("refreshexpired");

            var user = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Username == model.Username);
            if (user.UserStatus == UserStatusEnum.Blocked) throw new UserBlockedException("Пользователь заблокирован.");

            _context.RefreshTokens.Remove(refreshToken);

            return GetToken(user);
        }

        public TokenViewModel GetToken(User user)
        {
            var identity = GetIdentity(user);

            var token = GenerateToken(identity);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = AddRefreshToken(user);

            return new TokenViewModel
            {
                UserId = user.Id,
                Token = encodedJwt,
                RefreshToken = refreshToken.Id.ToString(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role.Name,
                ValidTo = token.ValidTo,
                ValidFrom = token.ValidFrom
            };
        }

        public void RestorePassword(RestorePasswordViewModel model)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == model.Email);
            if (user == null) throw new Exception("Пользователь с данным email не найден!");

            var password = PasswordGenerator.RandomPassword();
            user.PasswordHash = PasswordHasher.HashPassword(password);
            _emailService.SendEmailAsync(model.Email, "Восстановление пароля",
                "Ваш новый пароль для доступа в систему WEBAPI: " + password);

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
                new Claim("UserId", user.Id.ToString())
            };

            if (user.CompanyId.HasValue)
                claims.Add(
                    new Claim("CompanyId", user.CompanyId.Value.ToString()));

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        public TokenViewModel ConfirmIp(string username, IPAddress remoteIp, string code)
        {
            var user = _context.Users.Include(x => x.Role).FirstOrDefault(x => x.Username == username);
            if (user == null) throw new Exception("Пользователь не найден.");

            if (user.Code != code) throw new Exception("Код подтверждения введен неверно.");
            if (user.CodeExpire <= DateTime.UtcNow) throw new Exception("Код подтверждения истек.");

            var trustedIp = new TrustedIpAddress
            {
                Date = DateTime.UtcNow,
                IpAddress = remoteIp.ToString(),
                UserId = user.Id
            };
            _context.TrustedIpAddresses.Add(trustedIp);
            _context.SaveChanges();

            return GetToken(user);
        }

        public bool IsBlocked(int companyId)
        {
           return _context.Companies.Any(x => x.Id == companyId && x.IsBlocked);
        }

        public void LogOutCompany(int companyId)
        {
            var users = _context.Users.Include(x => x.RefreshTokens)
                                                    .Where(x => x.CompanyId == companyId)
                                                    .ToList();
            var tokens = new List<RefreshToken>();
            foreach (var user in users)
            {
                tokens.AddRange(user.RefreshTokens);
            }
 
            _context.RefreshTokens.RemoveRange(tokens);
            _context.SaveChanges();
        }

        private bool CheckIp(int userId, IPAddress remoteIp)
        {
            var user = _context.Users.Include(x => x.TrustedIpAddresses).FirstOrDefault(x => x.Id == userId);
            if (user == null) throw  new Exception("Пользователь не найден.");

            var allowedIps = user.TrustedIpAddresses.Select(x => x.IpAddress).ToList();
            var remoteIpBytes = remoteIp.GetAddressBytes();

            foreach (var address in allowedIps)
            {
                var allowedIp = IPAddress.Parse(address);
                if (allowedIp.GetAddressBytes().SequenceEqual(remoteIpBytes)) return true;
            }

            SendCode(user);
            return false;
        }

        private void TwoFactor(int userId)
        {
            var user = _context.Users.Include(x => x.TrustedIpAddresses).FirstOrDefault(x => x.Id == userId);
            if (user == null) throw new Exception("Пользователь не найден.");

            SendCode(user);
            throw new Exception("confirmsms");
        }

        private void SendCode(User user)
        {
            user.Code = new Random().Next(1000, 9999).ToString();
            user.CodeExpire = DateTime.UtcNow.AddSeconds(90);

            _context.Users.Update(user);
            _context.SaveChanges();

            _smsService.SendSms(new SendSmsViewModel
            {
                Message = user.Code,
                PhoneNumber = user.PhoneNumber
            });
        }

        private User AuthUser(GetTokenViewModel model)
        {
            var user = _context.Users.Include(x => x.Role).Include(x => x.Company).FirstOrDefault(x =>
                x.Username == model.Username);

            if (user == null) throw new UserNotFoundException("Пользователь не найден.");
            if (user.UserStatus == UserStatusEnum.Blocked) throw new UserBlockedException("Пользователь заблокирован.");
            if (user.Company != null && user.Company.IsBlocked) throw new Exception("Ваша компания заблокирована. Обратитесь к администрации.");

            if (PasswordHasher.VerifyPassword(user.PasswordHash, model.Password))
            {
                user.PasswordFailCount = 0;

                _context.Users.Update(user);
                _context.SaveChanges();

                return user;
            }
            else
            {
                user.PasswordFailCount += 1;

                _context.Users.Update(user);
                _context.SaveChanges();

                if (user.PasswordFailCount >= 3)
                {
                    throw new RefreshTokenNotFoundException("showpasswordcaptcha");
                }
            }

            throw new UserNotFoundException("Неверный пароль.");
        }

        private RefreshToken AddRefreshToken(User user)
        {
            var now = DateTime.UtcNow;
            var token = new RefreshToken
            {
                ValidTo = now.Add(TimeSpan.FromMinutes(int.Parse(Configuration["Jwt:LifeTimeMinutes"]) * 3000)),
                ValidFrom = now,
                UserId = user.Id
            };
            _context.RefreshTokens.Add(token);
            _context.SaveChanges();

            return token;
        }

        private JwtSecurityToken GenerateToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                Configuration["Jwt:Issuer"],
                Configuration["Jwt:Audience"],
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(int.Parse(Configuration["Jwt:LifeTimeMinutes"]) * 10000)),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256));
        }
    }
}
