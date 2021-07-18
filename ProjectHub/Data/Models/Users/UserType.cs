using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{
    public class UserType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.UserTypeNameMaxLength)]
        public string Name { get; set; }

    }
}