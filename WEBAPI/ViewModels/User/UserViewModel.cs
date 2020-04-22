using WEBAPI.Enums;
using WEBAPI.ViewModels.Role;
using WEBAPI.ViewModels.UserSettings;

namespace WEBAPI.ViewModels.User
{
    public class UserViewModel : UserInfoViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserStatusEnum UserStatus { get; set; }


        public RoleViewModel Role { get; set; }
        public UpdateAdministratorUserSettingsViewModel UserSettings { get; set; }
        public int[] AllowedUsers { get; set; }
    }
}
