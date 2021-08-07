using Microsoft.AspNetCore.Http;
using ProjectHub.Data.Models;
using ProjectHub.Models.Discussion;
using ProjectHub.Models.Project;
using ProjectHub.Models.Review;
using ProjectHub.Models.User;
using System.Collections.Generic;

namespace ProjectHub.Services.User
{
    public interface IUserService
    {
        public ApplicationUser GetUserById(int userId);
        object GetUserKindEntityByUserId(string userKind, int userId);

        public void EditUser(UserEditProfileViewModel model);

        public IEnumerable<ProjectListingViewModel> GetUserProjects(int id, string userKind);

        public IEnumerable<ReviewListingViewModel> GetUserReviews(int id);

        public IEnumerable<DiscussionViewModel> GetUserDiscussions(int id);
        public byte[] GetUserImage(int id);

        public byte[] ProcessUploadedFile(IFormFile file);

        public string GetUserRecommendationsCount(int authorId, int recipientId);
        public string GetUserDisapprovalsCount(int authorId, int recipientId);

        public bool CheckIfUserIsAlreadyReviewedByTheLoggedUser(int recipientId, int loggedUserId);
    }
}
