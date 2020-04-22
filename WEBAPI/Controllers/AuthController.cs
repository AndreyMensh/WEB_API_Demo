using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Auth;

namespace WEBAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST: api/Auth
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Post([FromBody] GetTokenViewModel model)
        {
            
            var ip = ControllerContext.HttpContext.Connection.RemoteIpAddress;
            return Ok(_authService.Token(model, ip));
        }

        // POST: api/Auth/mobile
        [AllowAnonymous]
        [HttpPost("mobile")]
        public IActionResult TokenMobile([FromBody] GetTokenViewModel model)
        {
            return Ok(_authService.TokenMobile(model));
        }

        // PUT: api/Auth/5
        [AllowAnonymous]
        [HttpPut]
        public IActionResult Put([FromBody] RefreshTokenViewModel model)
        {
            return Ok(_authService.Refresh(model));
        }

        // PUT: api/Auth/Restore
        [AllowAnonymous]
        [HttpPut("restore")]
        public IActionResult Restore([FromBody] RestorePasswordViewModel model)
        {
            _authService.RestorePassword(model);
            return Ok();
        }

        // PUT: api/Auth/confirmip
        [AllowAnonymous]
        [HttpPut("confirmip")]
        public IActionResult ConfirmIp([FromBody] ConfirmIpViewModel model)
        {
            return Ok(_authService.ConfirmIp(model.Username, ControllerContext.HttpContext.Connection.RemoteIpAddress, model.Code));
        }
    }
}
