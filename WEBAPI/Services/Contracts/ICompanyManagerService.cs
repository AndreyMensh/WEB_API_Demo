using System.Collections.Generic;
using System.Threading.Tasks;
using WEBAPI.ViewModels.Company;

namespace WEBAPI.Services.Contracts
{
    public interface ICompanyManagerService
    {
        CompanyViewModel Get(int id);
        List<CompanyViewModel> GetAll();
        CompanyViewModel Create(CreateCompanyViewModel model);
        Task Edit(int companyId, UpdateCompanyViewModel model);
        void Delete(int id);
        void TriggerBlock(int id);
    }
}
