using ProjectHub.Data;
using ProjectHub.Data.Models;
using ProjectHub.Services.Reviews;
using ProjectHub.Tests.Mocks;
using System.Linq;
using Xunit;

namespace ProjectHub.Tests.Services
{
    public class ReviewServiceTests
    {
        //ReviewExists

        [Fact]
        public void ReviewExistsMethodShouldReturnTrueIfReviewExists()
        {
            //Arrange
            var data = ProjectHubDbContextMock.Instance;
            var service = new ReviewService(data);

            data.Reviews.Add(new Review
            {
                AuthorId = 1,
                RecipientId = 2,
                Content = "Content"
            });

            data.SaveChanges();

            //Act
            bool result = service.ReviewExists(1, 2);

            //Assert
            Assert.Single(data.Reviews.ToList());
            Assert.True(result);
        }

        [Fact]
        public void ReviewExistsMethodShouldReturnFalseIfReviewDoesNotExists()
        {
            //Arrange
            var data = ProjectHubDbContextMock.Instance;
            var service = new ReviewService(data);

            //Act
            bool result = service.ReviewExists(1, 2);

            //Assert
            Assert.Empty(data.Reviews.ToList());
            Assert.False(result);
        }

        //AddReview

        [Fact]
        public void AddReviewShouldAddNewInstanceOfReviewTypeInTheDatabase()
        {
            //Arrange
            var data = ProjectHubDbContextMock.Instance;
            var service = new ReviewService(data);

            //Act
            service.AddReview(1, 2, "Content");
            var reviews = data.Reviews.ToList();

            //Assert
            Assert.Single(reviews);
            Assert.Equal(1, reviews.First().AuthorId);
            Assert.Equal(2, reviews.First().RecipientId);
            Assert.Equal("Content", reviews.First().Content);
        }

        //EditReview

        [Fact]
        public void EditReviewMethodShouldModifyReviewContent()
        {
            //Arrange
            var data = ProjectHubDbContextMock.Instance;
            var service = new ReviewService(data);

            data.Reviews.Add(new Review
            {
                AuthorId = 1,
                RecipientId = 2,
                Content = "FirstContent"
            });

            data.SaveChanges();

            //Act
            var expectetCOntent = "EditedContent";

            service.EditReview(1, 2, expectetCOntent);

            var reviewContent = data.Reviews.FirstOrDefault(r => r.AuthorId.Equals(1)
                                                     && r.RecipientId.Equals(2))
                                     .Content;

            //Assert
            Assert.Equal(expectetCOntent, reviewContent);
        }
    }
}
