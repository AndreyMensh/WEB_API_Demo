using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.CompanySettings;

namespace WEBAPI.Controllers
{
    [Route("api/companysettings")]
    [ApiController]
    public class CompanySettingsController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly ICompanySettingsService _companySettingsService;

        public CompanySettingsController(IHelperService helperService, ICompanySettingsService companySettingsService)
        {
            _helperService = helperService;
            _companySettingsService = companySettingsService;
        }

        // GET: api/CompanySettings
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_companySettingsService.Get(_helperService.GetCompanyId(User)));
        }

        // PUT: api/CompanySettings/5
        [Authorize(Roles = "Head")]
        [HttpPut]
        public IActionResult Put([FromBody] UpdateCompanySettingsViewModel model)
        {
            _companySettingsService.Update(model, _helperService.GetCompanyId(User));
            return Ok();
        }
    }
}
