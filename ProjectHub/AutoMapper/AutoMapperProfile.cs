using AutoMapper;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Project;
using ProjectHub.Models.User;
using System;
using System.Globalization;
using System.Linq;

namespace ProjectHub.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //USERS MAPPING
            CreateMap<ApplicationUser, AppUserProfileViewModel>()
                .ForMember(
                          u => u.UserKindName,
                          opt => opt.MapFrom(au => au.UserKind.Name));
                
            CreateMap<ApplicationUser, AppUserEditProfileViewModel>()
                .ForMember(
                          u => u.UserKindName,
                          opt => opt.MapFrom(au => au.UserKind.Name));

            CreateMap<object, UserProfileViewModel>();

            CreateMap<object, UserEditProfileViewModel>();

            CreateMap<Investor, UserProfileViewModel>();

            CreateMap<Investor, UserEditProfileViewModel>();

            CreateMap<Manager, UserProfileViewModel>();
            CreateMap<Manager, UserEditProfileViewModel>();

            CreateMap<Designer, UserProfileViewModel>();
            CreateMap<Designer, UserEditProfileViewModel>();

            CreateMap<Contractor, UserProfileViewModel>();
            CreateMap<Contractor, UserEditProfileViewModel>();


            //PROJECTS MAPPING
            CreateMap<Project, ProjectListingViewModel>()
                .ForMember(
                          pl => pl.Investor,
                          opt => opt.MapFrom(p => p.Investor.User.FullName))
                .ForMember(
                          p=> p.Deadline,
                          opt => opt.MapFrom(p=>p.Deadline.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)));
                
            
        }
    }
}
