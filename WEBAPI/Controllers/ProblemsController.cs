using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.ViewModels.Problem;

namespace WEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ProblemsController(IEmailService emailService)
        {
            _emailService = emailService;
        }


        // POST: api/Problems
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPost]
        public IActionResult Post([FromBody] AddProblemViewModel model)
        {
            if (!string.IsNullOrEmpty(model.Problem)) _emailService.SendEmailAsync("ggmon007@gmail.com", "Проблема", model.Problem);
            return Ok();
        }
    }
}
