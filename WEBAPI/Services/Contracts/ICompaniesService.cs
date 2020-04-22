using WEBAPI.ViewModels.Company;

namespace WEBAPI.Services.Contracts
{
    public interface ICompaniesService
    {
        CompanyContactInfoViewModel GetContactInfo(int id);
        void UpdateContactInfo(UpdateCompanyContactInfoViewModel model);
        bool UserInThisCompany(int userId, int companyId);
    }
}
