using ProjectHub.Data.Models.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Contractor
    {
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Activity> Activities { get; set; }

        public ICollection<ProjectContractor> Projects => new HashSet<ProjectContractor>();
    }
}
