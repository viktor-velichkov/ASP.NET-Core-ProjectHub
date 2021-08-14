using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Investor
    {
        public Investor()
        {
            this.Projects = new HashSet<Project>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
