namespace ProjectHub.Models.Offer
{
    public class OfferListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public int AuthorId { get; set; }

        public string Author { get; set; }

        public string Position { get; set; }

        public string Date { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}
