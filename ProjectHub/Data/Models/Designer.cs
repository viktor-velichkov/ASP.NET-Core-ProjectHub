using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Designer : User
    {
        
        [ForeignKey(nameof(Discipline))]
        public int DisciplineId { get; set; }

        [Required]
        public Discipline Discipline { get; set; }

        [Required]
        public int WorkExperience { get; set; }
    }
}
