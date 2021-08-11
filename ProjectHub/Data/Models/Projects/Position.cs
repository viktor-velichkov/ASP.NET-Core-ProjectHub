namespace ProjectHub.Data.Models.Projects
{
    public class Position
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public bool IsFree { get; set; }
    }
}
