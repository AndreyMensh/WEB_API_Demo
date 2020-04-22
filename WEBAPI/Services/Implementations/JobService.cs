using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Helpers.SearchModels;
using Microsoft.EntityFrameworkCore;
using WEBAPI.Enums;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.SearchModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Job;
using WEBAPI.ViewModels.Location;

namespace WEBAPI.Services.Implementations
{
    public class JobService : IJobService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ICompaniesService _companiesService;
        private readonly IBreakService _breakService;
        private readonly ICompanySettingsService _companySettingsService;

        public JobService(ApplicationDatabaseContext context, IMapper mapper, ICompaniesService companiesService, IBreakService breakService, ICompanySettingsService companySettingsService)
        {
            _context = context;
            _mapper = mapper;
            _companiesService = companiesService;
            _breakService = breakService;
            _companySettingsService = companySettingsService;
        }

        public SearchResult<Job> Search(JobSearchModel searchModel)
        {
            var user = _context.Users.Include(x => x.Role).Include(x => x.AllowedUsers)
                .FirstOrDefault(x => x.Id == searchModel.CurrentUserId);
            if (user != null)
            {
                searchModel.AllowedUsers = user.AllowedUsers.Select(x => x.AllowedUserId).ToList();
                searchModel.AllowedUsers.Add(user.Id);
            }

            if (user.Role.Name == "Head")
            {
                searchModel.AllowedUsers =
                    _context.Users.Where(x => x.CompanyId == user.CompanyId).Select(x => x.Id).ToList();
            }


            var query = _context.Jobs
                .Include(x => x.Acts)
                .Include(x => x.Breaks)
                .Include(x => x.User)
                .Include(x => x.StartLocation)
                .Include(x => x.EndLocation)
                .Where(x => !x.Deleted);
            return searchModel.Find(query);
        }

        public JobViewModel Create(int createdBy, int companyId, CreateJobViewModel model)
        {
            if (!_companiesService.UserInThisCompany(model.UserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");
            var item = _mapper.Map<CreateJobViewModel, Job>(model);

            item.CompanyId = companyId;

            item.CreatedAt = DateTime.UtcNow;
            item.CreatedBy = createdBy;
            item.DateStart = DateTime.UtcNow;
            item.JobStatus = JobStatusEnum.InProgress;

            item.Breaks = new List<Break>();
        
            _context.Jobs.Add(item);
            _context.SaveChanges();

            return _mapper.Map<Job, JobViewModel>(item);
        }

        public JobViewModel CreateFromCrm(int createdBy, int companyId, CreateJobCrmViewModel model)
        {
            if (!_companiesService.UserInThisCompany(model.UserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");
            var item = _mapper.Map<CreateJobViewModel, Job>(model);

            item.CompanyId = companyId;

            item.CreatedAt = DateTime.UtcNow;
            item.DateStart = model.DateStart;
            item.DateEnd = model.DateEnd;

            item.CreatedBy = createdBy;
            item.JobStatus = JobStatusEnum.Complete;

            _context.Jobs.Add(item);
            _context.SaveChanges();

            return _mapper.Map<Job, JobViewModel>(item);
        }

        public JobViewModel Get(int companyId, int currentId, int jobId)
        {
            if (!_companiesService.UserInThisCompany(currentId, companyId)) throw new Exception("У Вас нет прав на данное действие.");

            var job = GetJob(jobId);
            job.CompanyId = companyId;

            return _mapper.Map<Job, JobViewModel>(job);
        }

        private Job GetJob(int jobId)
        {
            var job = _context.Jobs
                .Include(x => x.User)
                .Include(x => x.Acts)
                .Include(x => x.Breaks)
                .Include(x => x.EndLocation)
                .Include(x => x.StartLocation)
                .FirstOrDefault(x => x.Id == jobId);
            if (job == null) throw new Exception("Работа не найдена.");

            return job;
        }


        public void Update(int currentUserId, int companyId, UpdateJobViewModel model)
        {
            if (!_companiesService.UserInThisCompany(model.UserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");

            var userSettings = _context.UserSettings.FirstOrDefault(x => x.UserId == currentUserId); 
            if (userSettings == null) throw new Exception("Настройки пользователя не найдены.");

            var job = _context.Jobs.Include(x => x.StartLocation).Include(x => x.EndLocation).Include(x => x.Acts).FirstOrDefault(x => x.Id == model.Id);
            if (job == null) throw new Exception("Объект не найден.");
            if (job.JobStatus == JobStatusEnum.SecondApprove) throw new Exception("Второе подтверждение получено. Никакие правки недвозможны.");

            if (userSettings.ManagerType != ManagerType.Two && job.JobStatus == JobStatusEnum.FirstApprove) throw new Exception("Редактирование невозможно. Первое подтверждение работы уже получено. Ожидайте дальнейшей обработки администрацией.");

            var settings = _context.CompanySettings.FirstOrDefault(x => x.CompanyId == companyId);
            if (settings == null) throw new Exception("Объект не найден.");
            if (settings.GpsRequired)
            {
                if (job.StartLocation == null && !model.IsFromCrm) throw new Exception("GPS обязательно.");
            }

            if (settings.WorkAtNigth && DateTime.UtcNow < settings.FromWork) throw new Exception("Вы не можете работать в данный момент.");

            if (model.DateStart.HasValue)
            {
                job.DateStart = model.DateStart.Value;
                job.DateStartManuallyChanged = model.DateStartManuallyChanged;
            }

            if (model.DateEnd.HasValue)
            {
                if (job.DateEnd.Value < job.DateStart) throw new Exception("Время окончания не может быть меньше времени начала работы.");
                job.DateEnd = model.DateEnd.Value;
                job.DateEndManuallyChanged = model.DateEndManuallyChanged;

                if (job.JobStatus != JobStatusEnum.FirstApprove && job.JobStatus != JobStatusEnum.SecondApprove)
                    job.JobStatus = JobStatusEnum.Complete;


                if (model.EndLocation == null && !model.IsFromCrm) throw new Exception("GPS обязательно.");
            }
            else
            {

                if (model.JobStatus == JobStatusEnum.Complete)
                {
                    //if (settings.ActRequired)
                    //{
                    //    if (job.Acts.Count == 0 && !model.IsFromCrm)
                    //    {
                    //        throw new Exception("Акт должен быть загружен!.");
                    //    }
                    //}

                    CompleteJob(job, model);
                }
            }

            job.Photo = model.Photo;
            job.Signature = model.Signature;
            job.Comment = model.Comment;
            job.Answer = model.Answer;
            job.Sum = model.Sum;
            job.ContactEmail = model.ContactEmail;
            job.ContactPhone = model.ContactPhone;
            job.CheckPhoto = model.CheckPhoto;

            job.ManagerComment = model.ManagerComment;

            if (settings.BreakTimeMinutes > 0 && job.JobStatus == JobStatusEnum.Complete)
                _breakService.CalculateBreakFinishTime(job, companyId);

            if (settings.MaximumWorkMinutes > 0 && job.JobStatus == JobStatusEnum.Complete)
            {
                var breaksDuration = BreaksDuration(job);
                var jobDuration = JobDuration(job);
                if ((jobDuration - breaksDuration) > settings.MaximumWorkMinutes)
                {
                    job.DateEnd = job.DateStart.AddMinutes(settings.MaximumWorkMinutes + breaksDuration);
                }
            }

            _context.Jobs.Update(job);
            _context.SaveChanges();

        }

        public JobViewModel Ping(int currentUserId, int companyId, UpdateJobViewModel model)
        {
            var job = GetJob(model.Id);
            var settings = _companySettingsService.Get(companyId);

            if (settings.MaximumWorkMinutes > 0)
            {
                var jobDuration = JobDuration(job);
                if (job.JobStatus == JobStatusEnum.InProgress && jobDuration > settings.MaximumWorkMinutes)
                {
                    CompleteJob(job, model);
                }
            }

            if (settings.WorkAtNigth)
            {
                if (job.JobStatus == JobStatusEnum.InProgress && DateTime.UtcNow > settings.ToWork)
                {
                    CompleteJob(job, model);
                }
            }

            return _mapper.Map<Job, JobViewModel>(job);
        }

        public void Approve(int currentUserId, int companyId, int jobId)
        {
            if (!_companiesService.UserInThisCompany(currentUserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");

            var job = _context.Jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null) throw new Exception("Данная работа не найдена.");
            var userSettings = _context.UserSettings.FirstOrDefault(x => x.UserId == currentUserId);
            if (userSettings == null) throw new Exception("Настройки пользователя не найдены.");

            if (job.JobStatus == JobStatusEnum.SecondApprove) throw new Exception("Работа уже получила второе подтверждение.");
            
            if (userSettings.ManagerType == ManagerType.One && job.JobStatus == JobStatusEnum.FirstApprove) throw new Exception("Вы менеджер первого уровня и не можете выдать второе подтверждение.");
            //if (userSettings.ManagerTypeOne && job.JobStatus == JobStatusEnum.FirstApprove) throw new Exception("Вы менеджер первого уровня и не можете выдать второе подтверждение.");
            //if (!userSettings.ManagerTypeTwo && job.JobStatus == JobStatusEnum.SecondApprove) throw new Exception("Второе подтверждение уже выдано.");
            if (userSettings.ManagerType == ManagerType.Two && job.JobStatus == JobStatusEnum.SecondApprove) throw new Exception("Второе подтверждение уже выдано.");

            var approveStatus = JobStatusEnum.FirstApprove;
            if (userSettings.ManagerType == ManagerType.Two)
            {
                if (job.JobStatus != JobStatusEnum.FirstApprove) throw new Exception("Не получено первое подтверждение. Вы можете дать только второе.");
                approveStatus = JobStatusEnum.SecondApprove;
            }
            
            job.JobStatus = approveStatus;
            job.ApproveDateTime = DateTime.UtcNow;


            _context.Jobs.Update(job);
            _context.SaveChanges();
        }

        public void DisApprove(int currentUserId, int companyId, int jobId)
        {
            if (!_companiesService.UserInThisCompany(currentUserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");

            var job = _context.Jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null) throw new Exception("Данная работа не найдена.");
            if (job.JobStatus == JobStatusEnum.SecondApprove) throw new Exception("Второе подтверждение получено. Никакие правки недвозможны.");
            var userSettings = _context.UserSettings.FirstOrDefault(x => x.UserId == currentUserId);
            if (userSettings == null) throw new Exception("Настройки пользователя не найдены.");

            if (userSettings.ManagerType == ManagerType.One)
            {
                switch (job.JobStatus)
                {
                    case JobStatusEnum.InProgress:
                        throw new Exception("Работа пока в процессе выполнения.");
                    case JobStatusEnum.SecondApprove:
                        throw new Exception("Второе подтверждение уже получено.");
                    case JobStatusEnum.FirstApprove:
                        throw new Exception("Первое подтверждение уже получено.");
                    case JobStatusEnum.Complete:
                        CancelWork(job);
                        break;
                }
            }
            if (userSettings.ManagerType == ManagerType.Two)
            {
                switch (job.JobStatus)
                {
                    case JobStatusEnum.InProgress:
                        throw new Exception("Работа пока в процессе выполнения.");
                    case JobStatusEnum.FirstApprove:
                        CancelWork(job);
                        break;
                }

                _context.Jobs.Update(job);
                _context.SaveChanges();
            }
        }

        private void CancelWork(Job job)
        {
            job.JobStatus = JobStatusEnum.Canceled;
            job.ApproveDateTime = DateTime.UtcNow;
            job.ApproverId = null;
        }

        public void Delete(int currentUserId, int companyId, int jobId)
        {
            if (!_companiesService.UserInThisCompany(currentUserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");
            if (!CanDelete(currentUserId, jobId)) throw new Exception("У Вас нет прав на данное действие.");

            var job = _context.Jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null) throw new Exception("Объект не найден.");
            
            job.Deleted = true;
            job.DeletedAt = DateTime.UtcNow;
            job.DeletedBy = currentUserId;

            _context.Jobs.Update(job);
            _context.SaveChanges();
        }

        private bool CanDelete(int userId, int jobId)
        {
            var job = _context.Jobs.FirstOrDefault(x => x.Id == jobId);
            if (job == null) throw new Exception("Объект не найден.");

            var user = _context.Users.Include(x => x.Role).Include(x => x.UserSettings).FirstOrDefault(x => x.Id == userId);
            if (user == null) throw new Exception("Пользователь не найден.");
            if (user.Role.Name == "Head") return true;

            if (user.UserSettings.ManagerType == ManagerType.One) return false;
            if (job.JobStatus == JobStatusEnum.SecondApprove) return false;

            return false;
        }

        private double JobDuration(Job job)
        {
            if (job.DateEnd.HasValue)
                return (job.DateEnd - job.DateStart).Value.TotalMinutes;

            return (DateTime.UtcNow - job.DateStart).TotalMinutes;
        }

        private double BreaksDuration(Job job)
        {
            if (job.Breaks == null) return 0;
            if (job.DateEnd.HasValue)
            {
                return job.Breaks.Sum(x => (x.DateEnd - x.DateStart).Value.TotalMinutes);
            }
            else
            {
                return job.Breaks.Sum(x => (DateTime.UtcNow - x.DateStart).Value.TotalMinutes);
            }
        }

        private void CompleteJob(Job job, UpdateJobViewModel model = null)
        {
            if (model != null)
                job.EndLocation = _mapper.Map<CreateLocationViewModel, Location>(model.EndLocation);

            job.JobStatus = JobStatusEnum.Complete;
            job.DateEnd = DateTime.UtcNow;

            _context.Jobs.Update(job);
            _context.SaveChanges();
        }
    }
}
