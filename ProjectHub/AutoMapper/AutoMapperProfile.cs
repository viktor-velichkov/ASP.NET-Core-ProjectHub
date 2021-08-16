using System.Linq;
using System.Globalization;
using AutoMapper;
using ProjectHub.Data.Models;
using ProjectHub.Data.Models.Projects;
using ProjectHub.Models.Contractor;
using ProjectHub.Models.Designer;
using ProjectHub.Models.Investor;
using ProjectHub.Models.Manager;
using ProjectHub.Models.Offer;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;

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
            CreateMap<Manager, ManagerListViewModel>()
                .ForMember(
                          mvm => mvm.Name,
                          opt => opt.MapFrom(m => m.User.FullName))
                .ForMember(
                          mvm => mvm.Image,
                          opt => opt.MapFrom(m => m.User.Image))
                .ForMember(
                          mvm => mvm.ProjectsCount,
                          opt => opt.MapFrom(m => m.Projects.Count()))
                .ForMember(
                           mvm => mvm.Recommendations,
                           opt => opt.MapFrom(m => m.User.RatesReceived.Count(rr => rr.IsPositive)))
                .ForMember(
                           mvm => mvm.Disapprovals,
                           opt => opt.MapFrom(m => m.User.RatesReceived.Count(rr => !rr.IsPositive)));


            //DESIGNERS
            CreateMap<Designer, UserProfileViewModel>();
            CreateMap<Designer, UserEditProfileViewModel>();
            CreateMap<Designer, DesignerListViewModel>()
                .ForMember(
                          mvm => mvm.Name,
                          opt => opt.MapFrom(m => m.User.FullName))
                .ForMember(
                          mvm => mvm.Image,
                          opt => opt.MapFrom(m => m.User.Image))
                .ForMember(
                          mvm => mvm.ProjectsCount,
                          opt => opt.MapFrom(m => m.Projects.Count()))
                .ForMember(
                           mvm => mvm.Recommendations,
                           opt => opt.MapFrom(m => m.User.RatesReceived.Count(rr => rr.IsPositive)))
                .ForMember(
                           mvm => mvm.Disapprovals,
                           opt => opt.MapFrom(m => m.User.RatesReceived.Count(rr => !rr.IsPositive)));


            //CONTRACTORS
            CreateMap<Contractor, UserProfileViewModel>();
            CreateMap<Contractor, UserEditProfileViewModel>();
            CreateMap<Contractor, ContractorListViewModel>()
                .ForMember(
                          mvm => mvm.Name,
                          opt => opt.MapFrom(m => m.User.FullName))
                .ForMember(
                          mvm => mvm.Image,
                          opt => opt.MapFrom(m => m.User.Image))
                .ForMember(
                          mvm => mvm.ProjectsCount,
                          opt => opt.MapFrom(m => m.Projects.Count()))
                .ForMember(
                           mvm => mvm.Recommendations,
                           opt => opt.MapFrom(m => m.User.RatesReceived.Count(rr => rr.IsPositive)))
                .ForMember(
                           mvm => mvm.Disapprovals,
                           opt => opt.MapFrom(m => m.User.RatesReceived.Count(rr => !rr.IsPositive)));


            //PROJECTS MAPPING
            CreateMap<Project, ProjectListingViewModel>()
                .ForMember(
                          pl => pl.Investor,
                          opt => opt.MapFrom(p => p.Investor.User.FullName))
                .ForMember(
                          p => p.Deadline,
                          opt => opt.MapFrom(p => p.Deadline.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture)));

            CreateMap<Project, ProjectCardViewModel>()
                .ForMember(
                          pl => pl.Investor,
                          opt => opt.MapFrom(p => p.Investor.User.FullName));

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

            //PROJECTDESIGNERS MAPPING
            CreateMap<ProjectDesigner, DesignerProjectDetailsViewModel>()
                .ForMember(
                           m => m.Name,
                           opt => opt.MapFrom(d => d.Designer.User.FullName))
                .ForMember(
                           m => m.Discipline,
                           opt => opt.MapFrom(d => d.Designer.Discipline.Name));


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
