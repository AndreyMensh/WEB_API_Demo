using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;

namespace WEBAPI.Controllers
{
    [Route("api/approves")]
    [ApiController]
    public class ApprovesController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly IJobService _jobService;

        public ApprovesController(IHelperService helperService, IJobService jobService)
        {
            _helperService = helperService;
            _jobService = jobService;
        }

        // PUT: api/Approves/5
        [Authorize(Roles = "Head, Administrator")]
        [HttpPut("{jobId}/{approve}")]
        public IActionResult Put(int jobId, bool approve)
        {
            if (approve) _jobService.Approve(_helperService.GetUserId(User), _helperService.GetCompanyId(User), jobId);
            else _jobService.DisApprove(_helperService.GetUserId(User), _helperService.GetCompanyId(User), jobId);
            return Ok();
        }
    }
}
