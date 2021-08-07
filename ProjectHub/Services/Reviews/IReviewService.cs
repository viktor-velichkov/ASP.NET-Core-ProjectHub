using ProjectHub.Data.Models;
using System.Collections.Generic;

namespace ProjectHub.Services.Reviews
{
    public interface IReviewService
    {
        public void AddReview(int authorId, int recipientId, string content);

        public List<Review> GetRecipientReviews(int recipientId);

        public void EditReview(int authorId, int recipientId, string content);

        public void RemoveReview(int recipientId, int authorId);
    }
}
