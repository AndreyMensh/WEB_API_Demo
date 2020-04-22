namespace WEBAPI.ViewModels.UserSettings
{
    public class UpdateUserSettingsViewModel
    {
        public bool AutomaticSchedule { get; set; }
        public bool ExcludeWeekends { get; set; }
        public bool WorkingShift { get; set; }
        public bool ExcludePublicHolidays { get; set; }


        public bool NotificationIfAbsentByEmail { get; set; }

        public bool CanSeeWorkingHours { get; set; }

        public int BreakDurationMinutes { get; set; }
        public int AfterWorkSubtractBreakMinutes { get; set; }

        public bool CanDeleteWork { get; set; }
        public bool CanEditWork { get; set; }


        public int UserId { get; set; }
    }
}
