using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.TableSettings;

namespace WEBAPI.Controllers
{
    [Route("api/tableSettings")]
    [ApiController]
    public class TableSettingsController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly ITableSettingsService _tableSettingsService;

        public TableSettingsController(IHelperService helperService, ITableSettingsService tableSettingsService)
        {
            _helperService = helperService;
            _tableSettingsService = tableSettingsService;
        }

        // GET: api/TableSettings
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_tableSettingsService.Get(_helperService.GetUserId(User)));
        }

        // PUT: api/TableSettings/5
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPut]
        public IActionResult Put([FromBody] UpdateTableSettingsViewModel model)
        {
            model.UserId = _helperService.GetUserId(User);
            _tableSettingsService.Update(model);
            return Ok();
        }
    }
}
