namespace ProjectHub.Models.Projects
{
    public class ProjectListingViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string City { get; set; }

        public int InvestorId { get; set; }

        public string Investor { get; set; }

        public string Deadline { get; set; }
    }
}
