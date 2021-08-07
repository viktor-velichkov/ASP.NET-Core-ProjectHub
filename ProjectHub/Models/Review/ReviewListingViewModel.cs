namespace ProjectHub.Models.Review
{
    public class ReviewListingViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }

        public int RecipientId { get; set; }

        public string Date { get; set; }

        public string Content { get; set; }
    }
}
