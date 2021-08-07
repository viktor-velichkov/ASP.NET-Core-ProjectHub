namespace ProjectHub.Models.User
{
    public class UserReviewsListViewModel
    {
        public int LoggedUserId { get; set; }
        public int Id { get; set; }

        public string UserKind { get; set; }

        public bool IsLoggedUser { get; set; }

        public bool AlreadyIsReviewedByTheLoggedUser { get; set; }

    }
}
