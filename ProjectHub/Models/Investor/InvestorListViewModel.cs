namespace ProjectHub.Models.Investor
{
    public class InvestorListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int ProjectsCount { get; set; }

        public int Recommendations { get; set; }

        public int Disapprovals { get; set; }

    }
}
