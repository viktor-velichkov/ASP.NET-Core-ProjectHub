using AutoMapper;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Project;
using ProjectHub.Models.User;
using System;
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
                          opt => opt.MapFrom(au => au.UserKind.Name))
                .ForMember(
                          u => u.Recomendations,
                          opt => opt.MapFrom(au => au.RatesReceived.Where(rr => rr.IsPositive).Count()))
                .ForMember(
                          u => u.Disapprovals,
                          opt => opt.MapFrom(au => au.RatesReceived.Where(rr => !rr.IsPositive).Count()));

            CreateMap<ApplicationUser, AppUserEditProfileViewModel>()
                .ForMember(
                          u => u.UserKindName,
                          opt => opt.MapFrom(au => au.UserKind.Name));

            CreateMap<object, UserProfileViewModel>();

            CreateMap<object, UserEditProfileViewModel>();

            CreateMap<Investor, UserProfileViewModel>();

            CreateMap<Investor, UserEditProfileViewModel>();

            CreateMap<Manager, UserProfileViewModel>();

            CreateMap<Designer, UserProfileViewModel>();

            CreateMap<Contractor, UserProfileViewModel>();


            //PROJECTS MAPPING
            CreateMap<Project, ProjectListingViewModel>()
                .ForMember(
                          p => p.Investor,
                          opt => opt.MapFrom(pl => pl.Investor.User.FullName));
                
            
        }
    }
}
