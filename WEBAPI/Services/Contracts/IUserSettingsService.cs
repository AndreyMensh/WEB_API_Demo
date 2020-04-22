using WEBAPI.Model.DatabaseModels;
using WEBAPI.ViewModels.UserSettings;

namespace WEBAPI.Services.Contracts
{
    public interface IUserSettingsService
    {
        UserSettings GetHeadSettings();
        UserSettingsViewModel Get(int companyId, int userId);
        void Update(UpdateUserSettingsViewModel model, int companyId, int userId);
    }
}
