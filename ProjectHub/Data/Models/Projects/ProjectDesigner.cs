using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models.Projects
{
    public class ProjectDesigner
    {
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(Designer))]
        public int DesignerId { get; set; }

        public Designer Designer { get; set; }
    }
}
