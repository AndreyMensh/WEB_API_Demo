using System;
using System.Linq;
using AutoMapper;
using WEBAPI.Enums;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;

namespace WEBAPI.Services.Implementations
{
    public class BreakService : IBreakService
    {

        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ICompaniesService _companiesService;
        private readonly ICompanySettingsService _companySettingsService;


        public BreakService(ApplicationDatabaseContext context, IMapper mapper, ICompaniesService companiesService, ICompanySettingsService companySettingsService)
        {
            _context = context;
            _mapper = mapper;
            _companiesService = companiesService;
            _companySettingsService = companySettingsService;
        }

        public void Create(int jobId, int companyId, int userId)
        {
            if (!_companiesService.UserInThisCompany(userId, companyId)) throw new Exception("У Вас нет прав на данное действие.");
            var job = _context.Jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null) throw new Exception("Работа не найдена!");
            if (job.JobStatus != JobStatusEnum.InProgress) throw new Exception("В данный момент пауза не доступна!");

            var breaks = _context.Breaks.Where(x => x.JobId == jobId && !x.Enabled);
            var settings = _companySettingsService.Get(companyId);

            var breaksDuration = breaks.Sum(x => (x.DateEnd - x.DateStart).Value.TotalMinutes);
            if (breaksDuration > settings.BreakTimeMinutes) throw new Exception("Лимит перерывов превышен!");

            var breakModel = new Break()
            {
                DateStart = DateTime.UtcNow,
                JobId = jobId,
                Enabled = true
            };

            job.JobStatus = JobStatusEnum.Paused;
            _context.Jobs.Update(job);
            _context.Breaks.Add(breakModel);
            _context.SaveChanges();
        }

        public void Finish(int jobId, int companyId, int userId)
        {
            if (!_companiesService.UserInThisCompany(userId, companyId)) throw new Exception("У Вас нет прав на данное действие.");
            var job = _context.Jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null) throw new Exception("Работа не найдена!");
            if (job.JobStatus != JobStatusEnum.Paused) throw new Exception("В данный невозможно отключить паузу!");

            var breaks = _context.Breaks.Where(x => x.JobId == jobId && x.Enabled);

            foreach (var item in breaks)
            {
                item.Enabled = false;
                item.DateEnd = DateTime.UtcNow;
            }

            job.JobStatus = JobStatusEnum.InProgress;

            _context.Jobs.Update(job);
            _context.Breaks.UpdateRange(breaks);

            _context.SaveChanges();
        }

        public void CalculateBreakFinishTime(Job job, int companyId)
        {
            var breaks = _context.Breaks.Where(x => x.JobId == job.Id && !x.Enabled);
            var settings = _companySettingsService.Get(companyId);
 

            var breaksDuration = breaks.Sum(x => (x.DateEnd - x.DateStart).Value.TotalMinutes);
            var jobDuration = (job.DateEnd - job.DateStart).Value.TotalMinutes;
            if (settings.BreakTimeMinutes > 0 && jobDuration > settings.SubtractBreakWorkMinutes)
            {
                if (breaksDuration < settings.BreakTimeMinutes)
                {
                    _context.Breaks.Add(new Break
                    {
                        JobId = job.Id,
                        Enabled = false,
                        DateStart = DateTime.UtcNow,
                        DateEnd = DateTime.UtcNow.AddMinutes(settings.BreakTimeMinutes - breaksDuration)
                    });
                    _context.SaveChanges();
                }
            }
        }
    }
}
