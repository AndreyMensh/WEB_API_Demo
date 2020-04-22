using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Break;

namespace WEBAPI.Controllers
{
    [Route("api/breaks")]
    [ApiController]
    public class BreaksController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly IBreakService _breakService;

        public BreaksController(IHelperService helperService, IBreakService breakService)
        {
            _helperService = helperService;
            _breakService = breakService;
        }

        // POST: api/Breaks
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateBreakViewModel model)
        {
            _breakService.Create(model.JobId, _helperService.GetCompanyId(User), _helperService.GetUserId(User));
            return Ok();
        }

        // PUT: api/Breaks/5
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPut("{jobId}")]
        public IActionResult Put(int jobId)
        {
            _breakService.Finish(jobId, _helperService.GetCompanyId(User), _helperService.GetUserId(User));
            return Ok();
        }
    }
}
