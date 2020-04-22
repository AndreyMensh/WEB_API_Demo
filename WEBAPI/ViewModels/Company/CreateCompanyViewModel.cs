using System.ComponentModel.DataAnnotations;
using WEBAPI.ViewModels.User;

namespace WEBAPI.ViewModels.Company
{
    public class CreateCompanyViewModel
    {
        [Required]
        public string CompanyName { get; set; }
        public string Notes { get; set; }
        [Required]
        public CreateUserViewModel User { get; set; }
    }
}
