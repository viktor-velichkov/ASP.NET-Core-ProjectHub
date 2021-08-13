using AutoMapper;
using ProjectHub.Data.Models;
using ProjectHub.Models.Investor;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
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

            //INVESTORS
            CreateMap<Investor, UserProfileViewModel>();
            CreateMap<Investor, UserEditProfileViewModel>();
            CreateMap<Investor, InvestorListViewModel>()
                .ForMember(
                          ivm => ivm.Name,
                          opt => opt.MapFrom(i => i.User.FullName))
                .ForMember(
                          ivm => ivm.Image,
                          opt => opt.MapFrom(i => i.User.Image))
                .ForMember(
                          ivm => ivm.ProjectsCount,
                          opt => opt.MapFrom(i => i.Projects.Count()))
                .ForMember(
                           ivm => ivm.Recommendations,
                           opt => opt.MapFrom(i => i.User.RatesReceived.Count(rr => rr.IsPositive)))
                .ForMember(
                           ivm => ivm.Disapprovals,
                           opt => opt.MapFrom(i => i.User.RatesReceived.Count(rr => !rr.IsPositive)));

            //MANAGERS
            CreateMap<Manager, UserProfileViewModel>();
            CreateMap<Manager, UserEditProfileViewModel>();


            //DESIGNERS
            CreateMap<Designer, UserProfileViewModel>();
            CreateMap<Designer, UserEditProfileViewModel>();
            CreateMap<Designer, DesignerProjectDetailsViewModel>()
                .ForMember(
                           m => m.Name,
                           opt => opt.MapFrom(d => d.User.FullName))
                .ForMember(
                           m => m.Discipline,
                           opt => opt.MapFrom(d => d.Discipline.Name));

            //CONTRACTORS
            CreateMap<Contractor, UserProfileViewModel>();
            CreateMap<Contractor, UserEditProfileViewModel>();

            //PROJECTS MAPPING
            CreateMap<Project, ProjectListingViewModel>()
                .ForMember(
                          pl => pl.Investor,
                          opt => opt.MapFrom(p => p.Investor.User.FullName))
                .ForMember(
                          p => p.Deadline,
                          opt => opt.MapFrom(p => p.Deadline.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Project, ProjectDetailsViewModel>()
                .ForMember(
                           pvm => pvm.Deadline,
                           opt => opt.MapFrom(p => p.Deadline.ToString("MMM dd, yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
                .ForMember(
                           pvm => pvm.Investor,
                           opt => opt.MapFrom(p => p.Investor.User.FullName))
                .ForMember(
                           pvm => pvm.Manager,
                           opt => opt.MapFrom(p => p.Manager.User.FullName))
                .ForMember(
                           pvm => pvm.Contractor,
                           opt => opt.MapFrom(p => p.Contractor.User.FullName));

            CreateMap<Project, ProjectOfferAddViewModel>()
                .ForMember(
                           pvm => pvm.Investor,
                           opt => opt.MapFrom(p => p.Investor.User.FullName));

            CreateMap<Project, ProjectOffersListViewModel>();
                            

            //REVIEWS MAPPING
            CreateMap<Review, ReviewListingViewModel>()
                .ForMember(
                          rl => rl.Author,
                          opt => opt.MapFrom(r => r.Author.FullName))
                .ForMember(
                          rl => rl.Date,
                          opt => opt.MapFrom(r => r.Date.ToString("HH:mm dd.MM.yyyy", CultureInfo.InvariantCulture)));

            //OFFERS MAPPING
            CreateMap<Offer, OfferListViewModel>()
                .ForMember(
                           offerViewModel => offerViewModel.Author,
                           opt => opt.MapFrom(offer => offer.Author.FullName))
                .ForMember(
                          offerViewModel => offerViewModel.Date,
                          opt => opt.MapFrom(offer => offer.Date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture))); ;


        }
    }
}
