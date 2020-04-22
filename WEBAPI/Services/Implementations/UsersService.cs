using System;
using System.Linq;
using AutoMapper;
using Helpers.Cryptography;
using Helpers.Cryptography.Cipher;
using Helpers.Exceptions;
using Helpers.SearchModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using WEBAPI.Enums;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.SearchModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.User;

namespace WEBAPI.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IRolesService _rolesService;

        public UsersService(ApplicationDatabaseContext context, IMapper mapper, IRolesService rolesService)
        {
            _context = context;
            _mapper = mapper;
            _rolesService = rolesService;
        }

        public CreatedUserViewModel Create(CreateUserViewModel model, RoleEnum role)
        {
            return _mapper.Map<User, CreatedUserViewModel>(CreateUser(model, role));
        }

        public SearchResult<User> Search(UserSearchModel searchModel)
        {
            var query = _context.Users.Include(x => x.Role).Include(x => x.UserSettings);
            return searchModel.Find(query);
        }

        public SearchResult<User> AlloweUsers(UserSearchModel searchModel, int userId)
        {
            var allowedUsers = _context.AllowedUsers.Where(x => x.UserId == userId).Select(x => x.AllowedUserId)
                .ToList();
            allowedUsers.Add(userId);

            searchModel.Take = 1000; //такнада
            searchModel.Ids = allowedUsers.ToArray();

            var query = _context.Users.Include(x => x.Role).Include(x => x.UserSettings);
            return searchModel.Find(query);
        }

        public void Edit(int userId, UpdateOrCreateUserViewModel model)
        {
            if (model.CompanyId == null) return;
            var user = GetUser(userId, model.CompanyId.Value).Include(x => x.AllowedUsers).Include(x => x.Role).Include(x => x.UserSettings).FirstOrDefault();

            var updatedUser = _mapper.Map<UpdateOrCreateUserViewModel, User>(model, user);

            if (!string.IsNullOrEmpty(model.Password))
            {
                updatedUser.PasswordHash = PasswordHasher.HashPassword(model.Password);
                updatedUser.PasswordEncrypted = AesOperation.DecryptString(Constants.Constant.PrivateKey, model.Password);
            }
                
            if (user.Role.Id != (int)RoleEnum.Head && (int) model.SelectedRole != user.Role.Id)
            {
               _rolesService.Assign(updatedUser, model.SelectedRole.ToString());
            }

            if (updatedUser.UserSettings.ManagerTypeOne)
                updatedUser.UserSettings.ManagerTypeTwo = false;
            if (updatedUser.UserSettings.ManagerTypeTwo)
                updatedUser.UserSettings.ManagerTypeOne = false;


            _context.Users.Update(updatedUser);
            _context.SaveChanges();


            if (model.AllowedUsers.Length > 0)
            {
                _context.AllowedUsers.RemoveRange(updatedUser.AllowedUsers);

                foreach (var allowedUser in model.AllowedUsers)
                {
                    updatedUser.AllowedUsers.Add(new AllowedUser
                    {
                        AllowedUserId = allowedUser,
                        UserId = updatedUser.Id,
                    });
                }

                _context.Users.Update(updatedUser);
                _context.SaveChanges();
            }

            _rolesService.ClearTokens(updatedUser.Id);
        }

        public void Delete(int id, int companyId)
        {
            _context.Users.Remove(GetUser(id, companyId).FirstOrDefault() ?? throw new UserNotFoundException());
            _context.SaveChanges();
            _rolesService.ClearTokens(id);
        }

        public UserViewModel Find(string username, int companyId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username && x.CompanyId == companyId);
            if (user != null)
            {
                return _mapper.Map<User, UserViewModel>(user);
            }
            throw new UserNotFoundException($"User with username {username} not found.");
        }

        public UserViewModel Find(int id, int companyId)
        {
            return _mapper.Map<User, UserViewModel>(GetUser(id, companyId).Include(x => x.Role).FirstOrDefault());
        }

        public IQueryable<User> GetUser(int id, int companyId)
        {
            if (!_context.Users.Any(x => x.Id == id && x.CompanyId == companyId)) throw new UserNotFoundException();

            return _context.Users
                .Include(x => x.AllowedUsers)
                .Include(x => x.Jobs)
                .Include(x => x.RefreshTokens)
                .Include(x => x.TrustedIpAddresses)
                .Include(x => x.UserSettings)
                .Include(x => x.TableSettings)
                .Where(x => x.Id == id && x.CompanyId == companyId);
        }

        public IQueryable<User> GetUser(string username, int companyId)
        {
            if (!_context.Users.Any(x => x.Username == username && x.CompanyId == companyId)) throw new UserNotFoundException();

            return _context.Users.Where(x => x.Username == username && x.CompanyId == companyId);
        }

        #region Private

        public User CreateUser(CreateUserViewModel model, RoleEnum role)
        {
            if (_context.Users.Any(x => x.Username == model.Email))
                throw new UserAlreadyExistsException($"Пользователь с email {model.Email} уже зарегистрирован.");
            var user = _mapper.Map<CreateUserViewModel, User>(model);
            
            user.PasswordHash = PasswordHasher.HashPassword(model.Password);
            user.PasswordEncrypted = AesOperation.EncryptString(Constants.Constant.PrivateKey, model.Password);

            user.CreatedAt = DateTime.UtcNow;
            user.LastUpdate = DateTime.UtcNow;
            user.Username = model.Email;


            _rolesService.Assign(user, role.GetDisplayName());

            if (role == RoleEnum.Worker)
            {
                user.UserSettings.CanAdministratorAct = true;
                user.UserSettings.CanAdministratorPhoto = true;
                user.UserSettings.CanAdministratorSignature = true;
                user.UserSettings.CanSeeWorkingHours = true;
                user.UserSettings.CanAdministratorCalendar = true;
            }

            _context.Users.Add(user);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            if (model.AllowedUsers == null || model.AllowedUsers.Length <= 0) return user;
            foreach (var allowedUser in model.AllowedUsers)
            {
                user.AllowedUsers.Add(new AllowedUser
                {
                    AllowedUserId = allowedUser,
                    UserId = user.Id,
                });
            }

            _context.Users.Update(user);
            _context.SaveChanges();

            return user;
        }

        #endregion
    }
}
