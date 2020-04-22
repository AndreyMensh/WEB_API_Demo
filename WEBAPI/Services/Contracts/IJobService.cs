using Helpers.SearchModels;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.SearchModels;
using WEBAPI.ViewModels.Job;

namespace WEBAPI.Services.Contracts
{
    public interface IJobService
    {
        SearchResult<Job> Search(JobSearchModel searchModel);
        JobViewModel Create(int createdBy, int companyId, CreateJobViewModel model);
        JobViewModel CreateFromCrm(int createdBy, int companyId, CreateJobCrmViewModel model);
        void Update(int currentUserId, int companyId, UpdateJobViewModel model);

        void Approve(int currentUserId, int companyId, int jobId);
        void DisApprove(int currentUserId, int companyId, int jobId);

        void Delete(int currentUserId, int companyId, int jobId);

        JobViewModel Ping(int currentUserId, int companyId, UpdateJobViewModel model);
    }
}
