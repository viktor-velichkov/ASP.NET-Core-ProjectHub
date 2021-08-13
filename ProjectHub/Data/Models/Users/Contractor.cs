using ProjectHub.Data.Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Contractor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Activity> Activities => new HashSet<Activity>();

        public ICollection<Project> Projects => new HashSet<Project>();
    }
}
