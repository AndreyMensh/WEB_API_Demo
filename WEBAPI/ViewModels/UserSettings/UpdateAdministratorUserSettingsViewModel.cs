using System;
using WEBAPI.Enums;

namespace WEBAPI.ViewModels.UserSettings
{
    public class UpdateAdministratorUserSettingsViewModel : UpdateUserSettingsViewModel
    {
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

        public DateTime? FromTimeCanSee { get; set; }
        public DateTime? ToTimeCanSee { get; set; }
        public int? DaysCanSee { get; set; }

        public bool ManagerTypeOne { get; set; }
        public bool ManagerTypeTwo { get; set; }

        public ManagerType ManagerType { get; set; }
    }
}
