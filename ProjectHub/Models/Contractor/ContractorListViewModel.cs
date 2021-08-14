namespace ProjectHub.Models.Contractor
{
    public class ContractorListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int ProjectsCount { get; set; }

        public int Recommendations { get; set; }

        public int Disapprovals { get; set; }

    }
}
