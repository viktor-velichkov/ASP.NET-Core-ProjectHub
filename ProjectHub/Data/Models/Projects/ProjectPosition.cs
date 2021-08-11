namespace ProjectHub.Data.Models.Projects
{
    public class ProjectPosition
    {
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
