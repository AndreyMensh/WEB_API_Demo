using System.ComponentModel.DataAnnotations;
using WEBAPI.Enums;
using WEBAPI.ViewModels.UserSettings;

namespace WEBAPI.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public int? CompanyId { get; set; }

        public RoleEnum SelectedRole { get; set; }
        public UserStatusEnum UserStatus { get; set; }

        public UpdateAdministratorUserSettingsViewModel UserSettings { get; set; }
        public int[] AllowedUsers { get; set; }
    }
}
