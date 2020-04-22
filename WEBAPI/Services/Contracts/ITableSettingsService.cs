using WEBAPI.ViewModels.TableSettings;

namespace WEBAPI.Services.Contracts
{
    public interface ITableSettingsService
    {
        TableSettingsViewModel Get(int userId);
        void Update(UpdateTableSettingsViewModel model);
    }
}
