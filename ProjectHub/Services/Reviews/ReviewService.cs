using Microsoft.EntityFrameworkCore;
using ProjectHub.Data;
using ProjectHub.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectHub.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly ProjectHubDbContext data;

        public ReviewService(ProjectHubDbContext data)
        {
            this.data = data;
        }

        public void AddReview(int authorId, int recipientId, string content)
        {
            this.data.Reviews.Add(new Review
            {
                AuthorId = authorId,
                RecipientId = recipientId,
                Date = DateTime.UtcNow,
                Content = content
            });

            this.data.SaveChanges();
        }

        public List<Review> GetRecipientReviews(int recipientId)
            => this.data
                  .Reviews
                  .Include(r => r.Author)
                  .Where(r => r.RecipientId.Equals(recipientId))
                  .ToList();

        public void EditReview(int authorId, int recipientId, string content)
        {
            var review = this.data
                             .Reviews
                             .FirstOrDefault(r => r.RecipientId.Equals(recipientId)
                                                  && r.AuthorId.Equals(authorId));

            review.Content = content;
            review.Date = DateTime.Now;

            this.data.SaveChanges();
        }

        public void RemoveReview(int recipientId, int authorId)
        {
            var review = this.data
                             .Reviews
                             .FirstOrDefault(r => r.RecipientId.Equals(recipientId)
                                                  && r.AuthorId.Equals(authorId));

            this.data.Reviews.Remove(review);

            this.data.SaveChanges();
        }
    }
}
