using System;
using WEBAPI.Enums;

namespace WEBAPI.Model.DatabaseModels
{
    public class UserSettings
    {
        public int Id { get; set; }

        public bool AutomaticSchedule { get; set; }
        public bool ExcludeWeekends { get; set; }
        public bool WorkingShift { get; set; }
        public bool ExcludePublicHolidays { get; set; }
        

        public bool NotificationIfAbsentByEmail { get; set; }

        public bool CanSeeWorkingHours { get; set; }

        public bool CanAdministratorAct { get; set; }
        public bool CanAdministratorPhoto { get; set; }
        public bool CanAdministratorSignature { get; set; }
        public bool CanAdministratorCalendar { get; set; }
        public bool CanAdministratorOnlyMonitoring { get; set; }
        public bool CanAdministratorComment { get; set; }
        public bool CanAdministratorSeeOnlyOnlineWorkers { get; set; }
        public bool CanAdministratorAllFunctionality { get; set; }

        public bool CanAdministratorWriteContactEmail { get; set; }
        public bool CanAdministratorWritePhone { get; set; }

        public bool CanAdministratorSettings { get; set; }
        public bool CanAdministratorWorkers { get; set; }
        public bool CanAdministratorAddTime { get; set; }

        public int BreakDurationMinutes { get; set; }
        public int AfterWorkSubtractBreakMinutes { get; set; }

        public bool ManagerTypeOne { get; set; }
        public bool ManagerTypeTwo { get; set; }

        public ManagerType ManagerType { get; set; }

        public DateTime? FromTimeCanSee { get; set; }
        public DateTime? ToTimeCanSee { get; set; }
        public int? DaysCanSee { get; set; }

        public bool CanDeleteWork { get; set; }
        public bool CanEditWork { get; set; }


        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
