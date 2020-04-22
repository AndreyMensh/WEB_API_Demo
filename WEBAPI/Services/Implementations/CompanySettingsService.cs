using System;
using System.Linq;
using AutoMapper;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.CompanySettings;

namespace WEBAPI.Services.Implementations
{
    public class CompanySettingsService : ICompanySettingsService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly ICompaniesService _companiesService;

        public CompanySettingsService(ApplicationDatabaseContext context, IMapper mapper, ICompaniesService companiesService)
        {
            _context = context;
            _mapper = mapper;
            _companiesService = companiesService;
        }

        public CompanySettingsViewModel Get(int companyId)
        {
            var companySettings = _mapper.Map<CompanySettings, CompanySettingsViewModel>(GetSettings(companyId));
            companySettings.CompanyContactInfo = _companiesService.GetContactInfo(companyId);
            return companySettings;
        }

        public void Update(UpdateCompanySettingsViewModel model, int companyId)
        {
            var contactInfo = model.CompanyContactInfo;
            contactInfo.Id = companyId;
            _companiesService.UpdateContactInfo(contactInfo);

            var companySettings = GetSettings(companyId);
            var updated = _mapper.Map<UpdateCompanySettingsViewModel, CompanySettings>(model, companySettings);
            if (updated.FromWork.HasValue)
                updated.FromWork = updated.FromWork.Value.ToUniversalTime();
            if (updated.ToWork.HasValue)
                updated.ToWork = updated.ToWork.Value.ToUniversalTime();

            updated.CompanyId = companyId;
            updated.Id = companySettings.Id;

            try
            {
                _context.CompanySettings.Update(updated);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        private CompanySettings GetSettings(int companyId)
        {
            var model = _context.CompanySettings.FirstOrDefault(x => x.CompanyId == companyId);
            if (model == null) throw new Exception("Настройки компании не найдены. Обратитесь к администрации.");

            return model;
        }
    }
}
