using AutoMapper;
using ProjectHub.Data.Models;
using ProjectHub.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserProfileViewModel>()
                .ForMember(
                          u => u.UserType,
                          opt => opt.MapFrom(au => au.UserType.ToString()))
                .ForMember(
                          u => u.Recomendations,
                          opt => opt.MapFrom(au => au.RatesReceived.Where(rr => rr.IsPositive).Count()))
                .ForMember(
                          u => u.Disapprovals,
                          opt => opt.MapFrom(au => au.RatesReceived.Where(rr => !rr.IsPositive).Count()));
        }
    }
}
