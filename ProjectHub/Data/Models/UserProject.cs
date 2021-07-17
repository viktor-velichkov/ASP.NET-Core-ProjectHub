namespace ProjectHub.Data.Models
{
    public class UserProject
    {
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}