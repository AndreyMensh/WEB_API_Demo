using WEBAPI.ViewModels.Location;

namespace WEBAPI.Services.Contracts
{
    public interface ILocationService
    {
        LocationViewModel Get(int id);
        void Create(CreateLocationViewModel model);
    }
}
