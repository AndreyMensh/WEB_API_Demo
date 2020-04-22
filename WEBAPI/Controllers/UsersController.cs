using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Enums;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.SearchModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.User;
using WEBAPI.ViewModels.UserSettings;

namespace WEBAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UsersController(IHelperService helperService, IUsersService usersService, IMapper mapper)
        {
            _helperService = helperService;
            _usersService = usersService;
            _mapper = mapper;
        }

        // GET: api/Users
        [Authorize(Roles = "Head, Administrator")]
        [HttpGet("{userId}")]
        public IActionResult Get(int userId)
        {
            return Ok(_usersService.Find(userId, _helperService.GetCompanyId(User)));
        }

        // GET: api/Users
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet("search")]
        public IActionResult Search([FromQuery]UserSearchModel searchModel)
        {
            searchModel.CompanyId = _helperService.GetCompanyId(User);
            var searchResult = _usersService.Search(searchModel);

            return Ok(new
            {
                Count = searchResult.Count,
                Body = _mapper.Map<List<User>, List<UserViewModel>>(searchResult.Body.ToList())
            });
        }

        // GET: api/AllowedUsers
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet("allowedUsers")]
        public IActionResult AllowedUsers([FromQuery]UserSearchModel searchModel)
        {
            searchModel.CompanyId = _helperService.GetCompanyId(User);
            var userId = _helperService.GetUserId(User);

            var searchResult = _usersService.AlloweUsers(searchModel, userId);

            return Ok(new
            {
                Count = searchResult.Count,
                Body = _mapper.Map<List<User>, List<UserViewModel>>(searchResult.Body.ToList())
            });
        }


        // POST: api/Users
        [Authorize(Roles = "Head, Administrator")]
        [HttpPost]
        public IActionResult Post([FromBody] UpdateOrCreateUserViewModel model)
        {

            CheckRoleToCreateOrUpdateUser(model);
            model.CompanyId = _helperService.GetCompanyId(User);


            if (model.UserId != 0) _usersService.Edit(model.UserId, model);
            else _usersService.Create(model, model.SelectedRole);
            return Ok();
        }

        // DELETE: api/Users/5
        [Authorize(Roles = "Head, Administrator")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var companyId = _helperService.GetCompanyId(User);

            _usersService.Delete(id, companyId);
        }

        private void CleanUpAdministratorFields(UpdateAdministratorUserSettingsViewModel model)
        {
            model.CanAdministratorAct = false;
            model.CanAdministratorComment = false;
            model.CanAdministratorAllFunctionality = false;
            model.CanAdministratorCalendar = false;
            model.CanAdministratorOnlyMonitoring = false;
            model.CanAdministratorPhoto = false;
            model.CanAdministratorSeeOnlyOnlineWorkers = false;
            model.CanAdministratorSignature = false;
        }

        private UpdateOrCreateUserViewModel CheckRoleToCreateOrUpdateUser(UpdateOrCreateUserViewModel model)
        {
            if (!User.IsInRole("Head") && (model.SelectedRole == RoleEnum.Administrator || model.SelectedRole == RoleEnum.Head)) throw new Exception("У Вас нет прав!");
            if (!User.IsInRole("Head")) CleanUpAdministratorFields(model.UserSettings);

            return model;
        }
    }
}
