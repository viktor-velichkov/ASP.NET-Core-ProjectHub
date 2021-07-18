using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models.Projects
{
    public class ProjectInvestor
    {
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(Investor))]
        public int InvestorId { get; set; }

        public Investor Investor { get; set; }
    }
}
