using ProjectHub.Data.Models;
using ProjectHub.Models.Discussion;
using ProjectHub.Models.Projects;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Services.User
{
    public interface IUserService
    {
        public bool IsUserExists(int userId);
        public List<Investor> GetTopThreeInvestors();
        public List<Manager> GetTopThreeManagers();
        public List<Designer> GetTopThreeDesigners();
        public List<Contractor> GetTopThreeContractors();
        
        public ApplicationUser GetUserById(int userId);

        public UserProfileViewModel GetUserProfileViewModel(int userId, string userKind);
        public UserEditProfileViewModel GetUserEditProfileViewModel(int userId, string userKind);

        public object GetUserKindEntityByUserId(string userKind, int userId);

        public void EditUser(UserEditProfileViewModel model);

        public IEnumerable<ProjectListingViewModel> GetUserProjects(int id, string userKind);

        public IEnumerable<ReviewListingViewModel> GetUserReviews(int id);

        public IEnumerable<DiscussionViewModel> GetUserDiscussions(int id);

        public Designer GetDesignerById(int id);

        public Discipline GetDesignerDiscipline(int id);

        public byte[] GetUserImage(int id);        

        public string GetUserRecommendationsCount(int authorId, int recipientId);

        public string GetUserDisapprovalsCount(int authorId, int recipientId);

        public bool ReviewAlreadyExists(int recipientId, int loggedUserId);

        public string GetPositionThatUserAppliesFor(int userId);

        public bool IsInRole(int userId, string roleName);

        public void RemoveUser(string userName);
    }
}
