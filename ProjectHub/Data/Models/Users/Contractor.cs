using ProjectHub.Data.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Contractor
    {
        public Contractor()
        {
            this.Activities = new HashSet<Activity>();
            this.Projects = new HashSet<Project>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public ICollection<Project> Projects { get; set; }
    }
}
