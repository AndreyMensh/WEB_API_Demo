using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Act;

namespace WEBAPI.Controllers
{
    [Route("api/acts")]
    [ApiController]
    public class ActsController : ControllerBase
    {
        private readonly IActsService _actsService;
        private readonly IHelperService _helperService;

        public ActsController(IActsService actsService, IHelperService helperService)
        {
            _actsService = actsService;
            _helperService = helperService;
        }

        // GET: api/Acts
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET: api/Acts/5
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet("{id}")]
        public IActionResult Get(int id, int jobId)
        {
            return Ok(_actsService.Get(id, jobId, _helperService.GetCompanyId(User)));
        }

        // POST: api/Acts
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateActViewModel model)
        {
            return Ok(_actsService.Create(_helperService.GetUserId(User), _helperService.GetCompanyId(User), model));
        }


        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _actsService.Delete(_helperService.GetUserId(User), _helperService.GetCompanyId(User), id);
            return Ok();
        }
    }
}
