using System;
using WEBAPI.ViewModels.User;

namespace WEBAPI.ViewModels.Company
{
    public class CompanyViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string PasswordDecrypted { get; set; }
        public string Notes { get; set; }
        public bool IsBlocked { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserViewModel GeneralUser { get; set; }
    }
}
