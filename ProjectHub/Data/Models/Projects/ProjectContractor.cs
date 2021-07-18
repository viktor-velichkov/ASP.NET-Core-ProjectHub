using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models.Projects
{
    public class ProjectContractor
    {
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(Contractor))]
        public int ContractorId { get; set; }

        public Contractor Contractor { get; set; }
    }
}
