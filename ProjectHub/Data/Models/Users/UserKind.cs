using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{
    public class UserKind
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.UserKindNameMaxLength)]
        public string Name { get; set; }

    }
}