using WEBAPI.ViewModels.User;

namespace WEBAPI.ViewModels.Company
{
    public class UpdateCompanyViewModel
    {
        public string CompanyName { get; set; }
        public string Notes { get; set; }

        public UpdateOrCreateUserViewModel User { get; set; }
    }
}
