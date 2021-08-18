using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Models.Discussion;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;


namespace ProjectHub.Services.User
{
    public class UserService : IUserService
    {
        private readonly ProjectHubDbContext data;
        private readonly IMapper mapper;

        public UserService(ProjectHubDbContext data,
                           IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public bool IsUserExists(int userId)
            => this.data.Users.Any(u => u.Id.Equals(userId));
        public List<Investor> GetTopThreeInvestors()
            => this.data
                   .Investors
                   .Include(i => i.User)
                   .ThenInclude(u => u.RatesReceived)
                   .Include(i => i.Projects)
                   .OrderByDescending(i => (double)i.User.RatesReceived.Count(rr => rr.IsPositive)
                                            - i.User.RatesReceived.Count(rr => !rr.IsPositive))
                   .Take(3)
                   .ToList();

        public List<Manager> GetTopThreeManagers()
            => this.data
                   .Managers
                   .Include(i => i.User)
                   .ThenInclude(u => u.RatesReceived)
                   .Include(i => i.Projects)
                   .OrderByDescending(i => (double)i.User.RatesReceived.Count(rr => rr.IsPositive)
                                            - i.User.RatesReceived.Count(rr => !rr.IsPositive))
                   .Take(3)
                   .ToList();

        public List<Designer> GetTopThreeDesigners()
            => this.data
                   .Designers
                   .Include(i => i.User)
                   .ThenInclude(u => u.RatesReceived)
                   .Include(i => i.Projects)
                   .OrderByDescending(i => (double)i.User.RatesReceived.Count(rr => rr.IsPositive)
                                            - i.User.RatesReceived.Count(rr => !rr.IsPositive))
                   .Take(3)
                   .ToList();

        public List<Contractor> GetTopThreeContractors()
            => this.data
                   .Contractors
                   .Include(i => i.User)
                   .ThenInclude(u => u.RatesReceived)
                   .Include(i => i.Projects)
                   .OrderByDescending(i => (double)i.User.RatesReceived.Count(rr => rr.IsPositive)
                                            - i.User.RatesReceived.Count(rr => !rr.IsPositive))
                   .Take(3)
                   .ToList();

        public ApplicationUser GetUserById(int userId)
             => this.data
                    .Users
                    .Include(u => u.UserKind)
                    .Include(u => u.RatesReceived)
                    .FirstOrDefault(u => u.Id.Equals(userId));

        public UserProfileViewModel GetUserProfileViewModel(int userId, string userKind)
        {
            var userKindEntity = this.GetUserKindEntityByUserId(userKind, userId);

            return this.mapper.Map<object, UserProfileViewModel>(userKindEntity);
        }

        public UserEditProfileViewModel GetUserEditProfileViewModel(int userId, string userKind)
        {
            var userKindEntity = this.GetUserKindEntityByUserId(userKind, userId);

            return this.mapper.Map<object, UserEditProfileViewModel>(userKindEntity);
        }

        public object GetUserKindEntityByUserId(string userKind, int userId)
        {
            object result = null;

            switch (userKind)
            {
                case "Investor":
                    result = this.data
                                 .Investors
                                 .Include(i => i.User)
                                 .Include(i => i.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Manager":
                    result = this.data
                                 .Managers
                                 .Include(m => m.User)
                                 .Include(m => m.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Designer":
                    result = this.data
                                 .Designers
                                 .Include(d => d.User)
                                 .Include(d => d.User.UserKind)
                                 .Include(d => d.Discipline)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                case "Contractor":
                    result = this.data
                                 .Contractors
                                 .Include(c => c.User)
                                 .Include(c => c.User.UserKind)
                                 .FirstOrDefault(i => i.UserId.Equals(userId));
                    break;
                default:
                    break;
            }

            return result;
        }

        public void EditUser(UserEditProfileViewModel model)
        {
            var userDb = this.data.Users.FirstOrDefault(u => u.Id.Equals(model.User.Id));

            userDb.Image = model.User.Image;
            userDb.FirstName = model.User.FirstName;
            userDb.LastName = model.User.LastName;
            userDb.Email = model.User.Email;
            userDb.PhoneNumber = model.User.PhoneNumber;
            userDb.WebSite = model.User.WebSite;
            userDb.Description = model.User.Description;
            userDb.FacebookPage = model.User.FacebookPage;
            userDb.LinkedinPage = model.User.LinkedinPage;
            userDb.SkypeProfile = model.User.SkypeProfile;

            UpdateUserKindEntityModel(model, userDb);

            this.data.SaveChanges();
        }

        public IEnumerable<ProjectListingViewModel> GetUserProjects(int id, string userKind)
        {
            List<Project> projects = new();

            switch (userKind)
            {
                case "Investor":
                    projects = this.data
                                   .Projects
                                   .Include(p => p.Investor)
                                   .ThenInclude(i => i.User)
                                   .Where(i => i.InvestorId.Equals(id))
                                   .ToList();
                    break;
                case "Manager":
                    projects = this.data
                                   .Projects
                                   .Include(p => p.Investor)
                                   .ThenInclude(i => i.User)
                                   .Where(i => i.ManagerId.Equals(id))
                                   .ToList();
                    break;
                case "Designer":
                    projects = this.data
                                   .Projects
                                   .Include(p => p.Investor)
                                   .ThenInclude(i => i.User)
                                   .Where(p => p.Designers.Select(pd => pd.DesignerId).Contains(id))
                                   .ToList();
                    break;
                case "Contractor":
                    projects = this.data
                                   .Projects
                                   .Include(p => p.Investor)
                                   .ThenInclude(i => i.User)
                                   .Where(i => i.ContractorId.Equals(id))
                                   .ToList();
                    break;
                default:
                    break;
            }

            return this.mapper.Map<List<Project>, List<ProjectListingViewModel>>(projects);
        }        

        public IEnumerable<ReviewListingViewModel> GetUserReviews(int id)
        {
            var userReviews = this.data
                                  .Reviews
                                  .Include(r => r.Author)
                                  .Where(r => r.RecipientId.Equals(id))
                                  .OrderByDescending(r => r.Date)
                                  .Select(r => this.mapper.Map<Review, ReviewListingViewModel>(r))
                                  .ToList();

            return userReviews;
        }

        public IEnumerable<DiscussionViewModel> GetUserDiscussions(int id)
        {
            var userDiscussions = this.data
                                      .UserDiscussions
                                      .Where(ud => ud.UserId.Equals(id))
                                      .Select(ud => ud.Discussion)
                                      .Select(d => this.mapper.Map<Discussion, DiscussionViewModel>(d))
                                      .ToList();
            return userDiscussions;
        }

        public Designer GetDesignerById(int id)
          => this.data.Designers.FirstOrDefault(d => d.Id.Equals(id));

        public Discipline GetDesignerDiscipline(int id)
            => this.data.Designers.Include(d => d.Discipline).FirstOrDefault(d => d.Id.Equals(id)).Discipline;

        public byte[] GetUserImage(int id)
            => this.data.Users.FirstOrDefault(u => u.Id.Equals(id)).Image;

        public string GetUserRecommendationsCount(int authorId, int recipientId)
        {
            var currentRate = this.data
                                  .Rates
                                  .FirstOrDefault(r => r.AuthorId.Equals(authorId)
                                                       && r.RecipientId.Equals(recipientId)
                                                       && r.IsPositive);
            if (currentRate == null)
            {
                this.data.Rates.Add(new Rate
                {
                    AuthorId = authorId,
                    RecipientId = recipientId,
                    IsPositive = true
                });
            }
            else
            {
                this.data.Rates.Remove(currentRate);
            }

            this.data.SaveChanges();

            return this.data.Rates.Where(r => r.RecipientId.Equals(recipientId) && r.IsPositive).Count().ToString();
        }

        public string GetUserDisapprovalsCount(int authorId, int recipientId)
        {
            var currentRate = this.data
                                  .Rates
                                  .FirstOrDefault(r => r.AuthorId.Equals(authorId)
                                                       && r.RecipientId.Equals(recipientId)
                                                       && !r.IsPositive);
            if (currentRate == null)
            {
                this.data.Rates.Add(new Rate
                {
                    AuthorId = authorId,
                    RecipientId = recipientId,
                    IsPositive = false
                });
            }
            else
            {
                this.data.Rates.Remove(currentRate);
            }

            this.data.SaveChanges();

            return this.data.Rates.Where(r => r.RecipientId.Equals(recipientId) && !r.IsPositive).Count().ToString();
        }

        public bool ReviewAlreadyExists(int recipientId, int loggedUserId)
            => this.data.Reviews.Any(r => r.AuthorId.Equals(loggedUserId)
                                          && r.RecipientId.Equals(recipientId));

        public string GetPositionThatUserAppliesFor(int userId)
        {
            var position = this.data
                               .Users
                               .Include(u => u.UserKind)
                               .First(u => u.Id.Equals(userId))
                               .UserKind
                               .Name;

            if (position.Equals(nameof(Designer)))
            {
                var disciplineName = this.GetDesignerDiscipline(userId).Name;

                position += $" - {disciplineName}";
            }

            return position;
        }

        public bool IsInRole(int userId, string roleName)
        {
            var role = this.data
                           .Roles
                           .FirstOrDefault(role => role.Name.Equals(roleName));

            return this.data
                       .UserRoles
                       .Any(ur => ur.RoleId.Equals(role.Id) && ur.UserId.Equals(userId));
        }

        public void RemoveUser(string userName)
        {
            var user = this.data.Users.FirstOrDefault(u => u.UserName.Equals(userName));

            this.data.Users.Remove(user);
        }

        private void UpdateUserKindEntityModel(UserEditProfileViewModel model, ApplicationUser user)
        {
            switch (model.User.UserKindName)
            {
                case "Investor":
                    var investor = this.data.Investors.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    investor.User = user;
                    break;
                case "Manager":
                    var manager = this.data.Managers.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    manager.User = user;
                    break;
                case "Designer":
                    var designer = this.data.Designers.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    designer.User = user;
                    designer.Discipline = model.Discipline;
                    designer.WorkExperience = model.WorkExperience;
                    break;
                case "Contractor":
                    var contractor = this.data.Contractors.FirstOrDefault(i => i.UserId.Equals(model.User.Id));
                    contractor.User = user;
                    break;
                default:
                    break;
            }
        }


    }
}
