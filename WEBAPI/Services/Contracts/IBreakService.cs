using WEBAPI.Model.DatabaseModels;

namespace WEBAPI.Services.Contracts
{
    public interface IBreakService
    {
        void Create(int jobId, int companyId, int userId);
        void Finish(int jobId, int companyId, int userId);

        void CalculateBreakFinishTime(Job job, int companyId);
    }
}
