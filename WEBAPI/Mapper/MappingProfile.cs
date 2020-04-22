using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.ViewModels.Act;
using WEBAPI.ViewModels.Company;
using WEBAPI.ViewModels.CompanySettings;
using WEBAPI.ViewModels.Job;
using WEBAPI.ViewModels.Location;
using WEBAPI.ViewModels.Role;
using WEBAPI.ViewModels.TableSettings;
using WEBAPI.ViewModels.User;
using WEBAPI.ViewModels.UserSettings;

namespace WEBAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserViewModel>()
                .ForMember(x => x.Role, o => o.MapFrom(src => src.Role))
                .ForMember(x => x.AllowedUsers, o => o.MapFrom(src => src.AllowedUsers.Select(x => x.AllowedUserId).ToArray()));
            CreateMap<User, CreatedUserViewModel>();
            CreateMap<CreateUserViewModel, User>()
                .ForMember(x => x.AllowedUsers, o => o.MapFrom(src => new List<AllowedUser>()));
            CreateMap<UpdateOrCreateUserViewModel, User>()
                .ForMember(t => t.Role, opt => opt.Ignore())
                .ForMember(x => x.AllowedUsers, o => o.MapFrom(src => new List<AllowedUser>()));

            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>();

            CreateMap<Company, CompanyViewModel>();
            CreateMap<Company, CompanyContactInfoViewModel>();
            CreateMap<CreateCompanyViewModel, Company>();


            CreateMap<UpdateCompanySettingsViewModel, CompanySettings>();
            CreateMap<CompanySettings, CompanySettingsViewModel>();

            CreateMap<UserSettings, UserSettingsViewModel>();
            CreateMap<UpdateUserSettingsViewModel, UserSettings>();
            CreateMap<UpdateAdministratorUserSettingsViewModel, UserSettings>()
                .ForMember(x => x.FromTimeCanSee, o => o.MapFrom(src => src.FromTimeCanSee.HasValue ? src.FromTimeCanSee.Value.ToUniversalTime() : DateTime.Now))
                .ForMember(x => x.ToTimeCanSee, o => o.MapFrom(src => src.ToTimeCanSee.HasValue ? src.ToTimeCanSee.Value.ToUniversalTime() : DateTime.Now)); ;

            CreateMap<Location, LocationViewModel>();
            CreateMap<Location, CreateLocationViewModel>();
            CreateMap<CreateLocationViewModel, Location>();


            CreateMap<UpdateTableSettingsViewModel, TableSettings>();
            CreateMap<TableSettings, TableSettingsViewModel>();

            CreateMap<CreateJobViewModel, Job>();
            CreateMap<UpdateJobViewModel, Job>();
            CreateMap<Job, JobViewModel>()
                .ForMember(x => x.Duration,
                    o => o.MapFrom(src => src.DateEnd.HasValue ? DurationMapper.FullDuration(src) : null))
                .ForMember(x => x.BreakDuration,
                    o => o.MapFrom(src => src.Breaks.Count > 0 ? DurationMapper.BreakDuration(src) : null))
                 .ForMember(x => x.JobDuration, o => o.MapFrom(src => DurationMapper.JobDuration(src)));

            CreateMap<CreateActViewModel, Act>();
            CreateMap<Act, ActViewModel>();
        }
    }
}