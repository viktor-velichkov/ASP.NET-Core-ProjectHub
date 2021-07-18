using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Offer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.OfferNameMaxLength)]
        public string Name { get; set; }

        
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}