using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Company;

namespace WEBAPI.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly ICompaniesService _companiesService;

        public CompaniesController(IHelperService helperService, ICompaniesService companiesService)
        {
            _helperService = helperService;
            _companiesService = companiesService;
        }

        // GET: api/Companies/5
        [Authorize(Roles = "Head")]
        [HttpGet]
        public IActionResult GetCompanyInfo()
        {
            return Ok(_companiesService.GetContactInfo(_helperService.GetCompanyId(User)));
        }


        // PUT: api/Companies/5
        [Authorize(Roles = "Head")]
        [HttpPut]
        public IActionResult Put([FromBody] UpdateCompanyContactInfoViewModel model)
        {
            model.Id = _helperService.GetCompanyId(User);
            _companiesService.UpdateContactInfo(model);
            return Ok();
        }
    }
}
