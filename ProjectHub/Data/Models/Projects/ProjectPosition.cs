using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class ProjectPosition
    {
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [ForeignKey(nameof(Position))]
        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
