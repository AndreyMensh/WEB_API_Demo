using WEBAPI.ViewModels.CompanySettings;

namespace WEBAPI.Services.Contracts
{
    public interface ICompanySettingsService
    {
        CompanySettingsViewModel Get(int companyId);
        void Update(UpdateCompanySettingsViewModel model, int companyId);
    }
}
