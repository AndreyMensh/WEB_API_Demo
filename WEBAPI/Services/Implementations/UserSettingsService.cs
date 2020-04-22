using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Enums;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.UserSettings;

namespace WEBAPI.Services.Implementations
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;

        public UserSettingsService(ApplicationDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserSettingsViewModel Get(int companyId, int userId)
        {
            var settings = _mapper.Map<UserSettings, UserSettingsViewModel>(GetSettings(companyId, userId));

            settings.CanAdministratorPhoto = false;

            if (settings.ManagerType == ManagerType.Three)
            {
                settings.CanAdministratorAct = false;
                settings.CanAdministratorComment = false;
                settings.CanAdministratorAllFunctionality = false;
                settings.CanAdministratorCalendar = false;
                settings.CanAdministratorSeeOnlyOnlineWorkers = false;
                settings.CanAdministratorSignature = false;

                settings.CanAdministratorOnlyMonitoring = true;
            }

            var user = _context.Users.Include(x => x.Role)
                .FirstOrDefault(x => x.Id == userId && x.CompanyId == companyId);
            if (user == null) throw new Exception("Настройки не найдены. Обратитесь к администратору.");
            if (user.Role.Name == "Head") settings.CanAdministratorPhoto = true;

            return settings;
        }

        public void Update(UpdateUserSettingsViewModel model, int companyId, int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId && x.CompanyId == companyId);
            if (user == null) throw new Exception("Невозможно сохранить настройки. Пользователь не найден или у Вас нет на это прав.");

            var userSettings = GetSettings(companyId, userId);

            var updated = _mapper.Map(model, userSettings);

            if (updated.CanAdministratorAllFunctionality)
            {
                updated.CanAdministratorAct = true;
                updated.CanAdministratorComment = true;
                updated.CanAdministratorCalendar = true;
                updated.CanAdministratorOnlyMonitoring = false;
                updated.CanAdministratorPhoto = true;
                updated.CanAdministratorSignature = true;
                updated.CanAdministratorSeeOnlyOnlineWorkers = false;
            }

            if (updated.ManagerTypeOne)
                updated.ManagerTypeTwo = false;
            if (updated.ManagerTypeTwo)
                updated.ManagerTypeOne = false;

            _context.UserSettings.Update(updated);
            _context.SaveChanges();
        }

        public UserSettings GetHeadSettings()
        {
            var settings = new UserSettings
            {
                CanAdministratorAct = true,
                CanAdministratorComment = true,
                CanAdministratorCalendar = true,
                CanAdministratorOnlyMonitoring = false,
                CanAdministratorPhoto = true,
                CanAdministratorSignature = true,
                CanAdministratorSeeOnlyOnlineWorkers = false,
                CanAdministratorAllFunctionality = true,
                CanAdministratorWriteContactEmail = true,
                CanAdministratorWritePhone = true,
                CanAdministratorSettings = true,
                CanAdministratorWorkers = true,
                CanAdministratorAddTime = true,

                DaysCanSee = 1000,
                FromTimeCanSee = DateTime.UtcNow,
                ToTimeCanSee = DateTime.UtcNow,


                AutomaticSchedule = true,
                ExcludeWeekends = true,
                WorkingShift = true,
                ExcludePublicHolidays = true,
                NotificationIfAbsentByEmail = true,
                CanSeeWorkingHours = true,

                BreakDurationMinutes = 0,
                AfterWorkSubtractBreakMinutes = 0,
                UserId = 0
            };


            return settings;
        }

        private UserSettings GetSettings(int companyId, int userId)
        {
            var model = _context.UserSettings.Include(x => x.User).FirstOrDefault(x => x.UserId == userId && x.User.CompanyId == companyId);
            if (model == null) throw new Exception("Настройки работника не найдены. Обратитесь к администрации.");

            return model;
        }
    }
}
