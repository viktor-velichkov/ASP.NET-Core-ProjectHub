namespace ProjectHub.Data.Models
{
    public class ProjectOffer
    {
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}