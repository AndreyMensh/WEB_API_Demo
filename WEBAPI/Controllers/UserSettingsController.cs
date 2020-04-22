using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.UserSettings;

namespace WEBAPI.Controllers
{
    [Route("api/usersettings")]
    [ApiController]
    public class UserSettingsController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly IUserSettingsService _userSettingsService;

        public UserSettingsController(IHelperService helperService, IUserSettingsService userSettingsService)
        {
            _helperService = helperService;
            _userSettingsService = userSettingsService;
        }

        // GET: api/UserSettings
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userSettingsService.Get(_helperService.GetCompanyId(User), _helperService.GetUserId(User)));
        }

        // GET: api/UserSettings/5
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet("{userId}")]
        public IActionResult GetUser(int userId)
        {
            return Ok(_userSettingsService.Get(_helperService.GetCompanyId(User), userId));
        }

        // PUT: api/UserSettings/5
        [Authorize(Roles = "Head, Administrator")]
        [HttpPut("{userId}")]
        public IActionResult Put(int userId, [FromBody] UpdateUserSettingsViewModel model)
        {
            model.UserId = userId;
            _userSettingsService.Update(model, _helperService.GetCompanyId(User), userId);
            return Ok();
        }

        // PUT: api/UserSettings/5
        [Authorize(Roles = "Head")]
        [HttpPut("administrator/{userId}")]
        public IActionResult PutAdministrator(int userId, [FromBody] UpdateAdministratorUserSettingsViewModel model)
        {
            _userSettingsService.Update(model, _helperService.GetCompanyId(User), userId);
            return Ok();
        }
    }
}
