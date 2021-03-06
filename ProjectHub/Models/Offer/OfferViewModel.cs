namespace ProjectHub.Models.Offer
{
    public class OfferViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Author { get; set; }

        public string Project { get; set; }
        
        public decimal Price { get; set; }
        
        public string Description { get; set; }
    }
}
