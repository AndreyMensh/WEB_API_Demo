using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Helpers.Cryptography;
using Helpers.Cryptography.Cipher;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Enums;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Company;
using WEBAPI.ViewModels.User;

namespace WEBAPI.Services.Implementations
{
    public class CompanyManagerService : ICompanyManagerService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUsersService _userService;
        private readonly IUserSettingsService _userSettingsService;

        public CompanyManagerService(ApplicationDatabaseContext context, IMapper mapper, IUsersService userService, IUserSettingsService userSettingsService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _userSettingsService = userSettingsService;
        }

        public List<CompanyViewModel> GetAll()
        {
            return Query().ToList();
        }

        public CompanyViewModel Get(int id)
        {

            var item = Query().FirstOrDefault(x => x.Id == id);
            return item;
        }

        private IQueryable<CompanyViewModel> Query()
        {
            var query = from company in _context.Companies
                join user in _context.Users.Include(x => x.Role) on company.GeneralUserId equals user.Id
                let decryptedPassword = AesOperation.DecryptString(Constants.Constant.PrivateKey, user.PasswordEncrypted)
                select new CompanyViewModel()
                {
                    Id = company.Id,
                    Name = company.Name,
                    Notes = company.Notes,
                    PasswordDecrypted = decryptedPassword,
                    CreatedAt = company.CreatedAt,
                    IsBlocked = company.IsBlocked,
                    GeneralUser = _mapper.Map<User, UserViewModel>(user)
                };

            return query;
        }

        public CompanyViewModel Create(CreateCompanyViewModel model)
        {
            model.User.CompanyId = null;
            var createdUser = _userService.Create(model.User, RoleEnum.Head);
            var user = _context.Users.FirstOrDefault(x => x.Id == createdUser.Id);
            if (user == null) throw new Exception("Error while creating user!");

            var company = new Company
            {
                Name = model.CompanyName,
                Notes = model.Notes,
                GeneralUserId = user.Id,
                CreatedAt = DateTime.UtcNow
            };
            _context.Companies.Add(company);

            user.CompanyId = company.Id;
            user.UserStatus = UserStatusEnum.Active;
            user.UserSettings = _userSettingsService.GetHeadSettings();

            _context.Users.Update(user);
            _context.SaveChanges();

            user.UserSettings.UserId = user.Id;
            _context.UserSettings.Update(user.UserSettings);
            _context.SaveChanges();

            company.GeneralUserId = user.Id;
            return _mapper.Map<Company, CompanyViewModel>(company);
        }



        public async Task Edit(int companyId, UpdateCompanyViewModel model)
        {
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == companyId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == model.User.UserId);

            if (company == null) throw new Exception("Компания не найдена.");
            if (user == null) throw new Exception("Пользователь не найден.");

            company.Name = model.CompanyName;
            company.Notes = model.Notes;

            user.FirstName = model.User.FirstName;
            user.LastName = model.User.LastName;
            user.PhoneNumber = model.User.PhoneNumber;

            if (user.Email != model.User.Email)
            {
                if (_context.Users.Any(x => x.Email == model.User.Email)) throw new Exception("Пользователь с таким email уже зарегистрирован.");
                user.Email = model.User.Email;
            }

            if (!string.IsNullOrEmpty(model.User.Password))
                user.PasswordHash = PasswordHasher.HashPassword(model.User.Password);

            _context.Companies.Update(company);
            _context.Users.Update(user);

            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var company = GetCompany(id)
                .Include(x => x.Users)
                .ThenInclude(x => x.UserSettings)
                .FirstOrDefault();

            _context.Companies.Remove(company);
            _context.Users.RemoveRange(company.Users);
            _context.SaveChanges();
        }

        public void TriggerBlock(int id)
        {
            var company = GetCompany(id).FirstOrDefault();
            company.IsBlocked = !company.IsBlocked;
            _context.Companies.Update(company);
            _context.SaveChanges();
        }

        private IQueryable<Company> GetCompany(int id)
        {
            if (!_context.Companies.Any(x => x.Id == id)) throw new Exception("Company not found");

            return _context.Companies.Where(x => x.Id == id);
        }
    }
}
