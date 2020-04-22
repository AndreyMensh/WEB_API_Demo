using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Helpers.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.SearchModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Job;

namespace WEBAPI.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IHelperService _helperService;
        private readonly IJobService _jobService;
        private readonly IUserSettingsService _userSettingsService;
        private readonly IMapper _mapper;

        public JobsController(IHelperService helperService, IJobService jobService, IMapper mapper, IUserSettingsService userSettingsService)
        {
            _helperService = helperService;
            _jobService = jobService;
            _mapper = mapper;
            _userSettingsService = userSettingsService;
        }

        // GET: api/Jobs
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpGet("search")]
        public IActionResult Search([FromQuery]JobSearchModel searchModel)
        {
            searchModel.CompanyId = _helperService.GetCompanyId(User);
            searchModel.CurrentUserId = _helperService.GetUserId(User);

            var settings = _userSettingsService.Get(searchModel.CompanyId, _helperService.GetUserId(User));
            searchModel.DaysCanSee = settings.DaysCanSee;
            searchModel.FromTimeCanSee = settings.FromTimeCanSee;
            searchModel.ToTimeCanSee = settings.ToTimeCanSee;

            var searchResult = _jobService.Search(searchModel);

            var entities = searchResult.Body.ToList();
            

            var body = _mapper.Map<List<Job>, List<JobViewModel>>(entities);
            var userGroups = from job in body
                              group job by job.UserId;

            var result = new List<GroupableJob>();
            foreach (IGrouping<int, JobViewModel> g in userGroups)
            {
                var jobs = new List<JobViewModel>();
                foreach (var t in g)
                    jobs.Add(t);

                result.Add(new GroupableJob
                {
                    GroupingEntity = g.Key.ToString(),
                    GroupingEntityValue = jobs[0].User.FirstName + " "+ jobs[0].User.LastName,

                    FullDuration = new DurationViewModel
                    {
                        AllMinutes = jobs.Sum(x => x.Duration?.AllMinutes ?? 0),
                        Hours = Math.Floor(jobs.Sum(x => x.Duration?.AllMinutes ?? 0) / 60),
                        Minutes = Math.Floor(jobs.Sum(x => x.Duration?.AllMinutes ?? 0) - Math.Floor(jobs.Sum(x => x.Duration?.AllMinutes ?? 0) / 60) * 60)
                    },
                    JobDuration = new DurationViewModel
                    {
                        AllMinutes = jobs.Sum(x => x.JobDuration?.AllMinutes ?? 0),
                        Hours = Math.Floor(jobs.Sum(x => x.JobDuration?.AllMinutes ?? 0) / 60),
                        Minutes = Math.Floor(jobs.Sum(x => x.JobDuration?.AllMinutes ?? 0) - Math.Floor(jobs.Sum(x => x.JobDuration?.AllMinutes ?? 0) / 60) * 60)
                    },
                    BreakDuration = new DurationViewModel
                    {
                        AllMinutes = jobs.Sum(x => x.BreakDuration?.AllMinutes ?? 0),
                        Hours = Math.Floor(jobs.Sum(x => x.BreakDuration?.AllMinutes ?? 0) / 60),
                        Minutes = Math.Floor(jobs.Sum(x => x.BreakDuration?.AllMinutes ?? 0) - Math.Floor(jobs.Sum(x => x.BreakDuration?.AllMinutes ?? 0) / 60) * 60)
                    },

                    Jobs = jobs
                });
            }

            return Ok(new {
                body = result,
                JobDuration = new DurationViewModel
                {
                    AllMinutes = result.Sum(x => x.JobDuration.AllMinutes),
                    Hours = Math.Floor(result.Sum(x => x.JobDuration.AllMinutes) / 60),
                    Minutes = result.Sum(x => x.JobDuration.AllMinutes) - Math.Floor(result.Sum(x => x.JobDuration.AllMinutes) / 60) * 60
                },
                FullDuration = new DurationViewModel
                {
                    AllMinutes = result.Sum(x => x.FullDuration.AllMinutes),
                    Hours = Math.Floor(result.Sum(x => x.FullDuration.AllMinutes) / 60),
                    Minutes = result.Sum(x => x.FullDuration.AllMinutes) - Math.Floor(result.Sum(x => x.FullDuration.AllMinutes) / 60) * 60
                },
                BreakDuration = new DurationViewModel
                {
                    AllMinutes = result.Sum(x => x.BreakDuration.AllMinutes),
                    Hours = Math.Floor(result.Sum(x => x.BreakDuration.AllMinutes) / 60),
                    Minutes = result.Sum(x => x.BreakDuration.AllMinutes) - Math.Floor(result.Sum(x => x.BreakDuration.AllMinutes) / 60) * 60
                }
            });
        }

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Jobs
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPost]
        public IActionResult Post([FromBody] CreateJobViewModel model)
        {
            if (model.UserId == 0) model.UserId = _helperService.GetUserId(User);
            return Ok(_jobService.Create(_helperService.GetUserId(User), _helperService.GetCompanyId(User), model));
        }

        // POST: api/Jobs
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPost("crm")]
        public IActionResult Post([FromBody] CreateJobCrmViewModel model)
        {
            return Ok(_jobService.CreateFromCrm(_helperService.GetUserId(User), _helperService.GetCompanyId(User), model));
        }

        // PUT: api/Jobs/5
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateJobViewModel model)
        {
            model.Id = id;
            _jobService.Update(_helperService.GetUserId(User), _helperService.GetCompanyId(User), model);
            return Ok();
        }

        // PUT: api/Jobs/ping/5
        [Authorize(Roles = "Head, Administrator, Worker")]
        [HttpPut("ping/{id}")]
        public IActionResult Ping(int id, [FromBody] UpdateJobViewModel model)
        {
            model.Id = id;

            return Ok(_jobService.Ping(_helperService.GetUserId(User), _helperService.GetCompanyId(User), model));
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "Head, Administrator")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userId = _helperService.GetUserId(User);
            var companyId = _helperService.GetCompanyId(User);
            _jobService.Delete(userId, companyId, id);
            return Ok();
        }
    }
}
