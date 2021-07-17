using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{
    public class Discipline
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}