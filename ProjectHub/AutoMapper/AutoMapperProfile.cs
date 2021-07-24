using AutoMapper;
using ProjectHub.Data.Models;
using ProjectHub.Models.Contractor;
using ProjectHub.Models.Designer;
using ProjectHub.Models.Investor;
using ProjectHub.Models.Manager;
using ProjectHub.Models.User;
using System.Linq;

namespace ProjectHub.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
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

            CreateMap<object, UserProfileViewModel>();

            CreateMap<Investor, UserProfileViewModel>();

            CreateMap<Manager, UserProfileViewModel>();

            CreateMap<Contractor, UserProfileViewModel>();

            CreateMap<Designer, UserProfileViewModel>();                
        }
    }
}
