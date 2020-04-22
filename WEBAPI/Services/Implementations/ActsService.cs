using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Act;

namespace WEBAPI.Services.Implementations
{
    public class ActsService : IActsService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ICompaniesService _companiesService;

        public ActsService(ApplicationDatabaseContext context, IMapper mapper, ICompaniesService companiesService)
        {
            _context = context;
            _mapper = mapper;
            _companiesService = companiesService;
        }

        public List<ActViewModel> GetAll(int jobId)
        {
            var models = _context.Acts.Where(x => x.JobId == jobId).ToList();
            return _mapper.Map<List<Act>, List<ActViewModel>>(models);
        }

        public ActViewModel Get(int id, int jobId, int companyId)
        {
            var job = _context.Jobs.FirstOrDefault(x => x.CompanyId == companyId && x.Id == id);
            if (job == null) throw new Exception("У Вас нет прав на данное действие.");

            var model = _context.Acts.FirstOrDefault(x => x.Id == id && x.JobId == jobId);
            return _mapper.Map<Act, ActViewModel>(model);
        }

        public ActViewModel Create(int currentUserId, int companyId, CreateActViewModel model)
        {
            if (!_companiesService.UserInThisCompany(currentUserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");
            var act = _mapper.Map<CreateActViewModel, Act>(model);

            act.CreatedBy = currentUserId;
            act.CreatedAt = DateTime.UtcNow;

            _context.Acts.Add(act);
            _context.SaveChanges();

            return _mapper.Map<Act, ActViewModel>(act);
        }

        public void Delete(int currentUserId, int companyId, int actId)
        {
            if (!_companiesService.UserInThisCompany(currentUserId, companyId)) throw new Exception("У Вас нет прав на данное действие.");
            var act = _context.Acts.FirstOrDefault(x => x.Id == actId);
            if (act == null) throw new Exception("Объект не найден.");

            _context.Acts.Remove(act);
            _context.SaveChanges();
        }
    }
}
