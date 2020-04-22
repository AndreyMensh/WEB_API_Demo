using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Company;

namespace WEBAPI.Controllers
{
    [Route("api/companiesmanager")]
    [ApiController]
    public class CompaniesMangerController : ControllerBase
    {
        private readonly ICompanyManagerService _companyManagerService;

        public CompaniesMangerController(ICompanyManagerService companyManagerService)
        {
            _companyManagerService = companyManagerService;
        }

        // GET: api/Companies
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_companyManagerService.GetAll());
        }

        // GET: api/Companies/1
        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("{id}")]
        public IActionResult GeById(int id)
        {
            var item = _companyManagerService.Get(id);

            return Ok(item);
        }

        // POST: api/Companies
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateCompanyViewModel model)
        {
            var items = _companyManagerService.Create(model);

            return Ok(items);
        }

        // POST: api/Companies
        [Authorize(Roles = "SuperAdmin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCompanyViewModel model)
        {
            await _companyManagerService.Edit(id, model);

            return Ok();
        }

        // PATCH: api/Companies
        [Authorize(Roles = "SuperAdmin")]
        [HttpPatch("{id}")]
        public IActionResult Patch(int id)
        {
            _companyManagerService.TriggerBlock(id);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _companyManagerService.Delete(id);
            return Ok();
        }
    }
}
