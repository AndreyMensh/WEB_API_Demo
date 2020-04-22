using System;

namespace WEBAPI.ViewModels.User
{
    public class UserInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
