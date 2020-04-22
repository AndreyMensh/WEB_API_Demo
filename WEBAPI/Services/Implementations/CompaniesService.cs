using System;
using System.Linq;
using AutoMapper;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.ViewModels.Company;

namespace WEBAPI.Services.Implementations
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ApplicationDatabaseContext _context;
        private readonly IMapper _mapper;

        public CompaniesService(ApplicationDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public CompanyContactInfoViewModel GetContactInfo(int id)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null) throw new Exception("Компания с id " + id + " не найдена!");

            return _mapper.Map<Company, CompanyContactInfoViewModel>(company);
        }

        public void UpdateContactInfo(UpdateCompanyContactInfoViewModel model)
        {
            var company = _context.Companies.FirstOrDefault(x => x.Id == model.Id);
            if (company == null) throw new Exception("Компания с id " + model.Id + " не найдена!");

            company.Name = model.Name;
            company.BillEmail = model.BillEmail;
            company.ContactEmail = model.ContactEmail;
            company.ContactName = model.ContactName;
            company.ContactPhone = model.ContactPhone;

            _context.Companies.Update(company);
            _context.SaveChanges();
        }

        public bool UserInThisCompany(int userId, int companyId)
        {
            return _context.Users.Any(x => x.Id == userId && x.CompanyId == companyId);
        }
    }
}
