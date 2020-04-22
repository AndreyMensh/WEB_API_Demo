using System.Collections.Generic;
using WEBAPI.ViewModels.Act;

namespace WEBAPI.Services.Contracts
{
    public interface IActsService
    {
        List<ActViewModel> GetAll(int jobId);
        ActViewModel Get(int id, int jobId, int companyId);
        ActViewModel Create(int currentUserId, int companyId, CreateActViewModel model);
        void Delete(int currentUserId, int companyId, int actId);
    }
}
