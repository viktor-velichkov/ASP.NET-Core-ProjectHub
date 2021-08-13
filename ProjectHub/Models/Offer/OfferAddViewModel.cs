using ProjectHub.Data;
using ProjectHub.Models.Projects;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.Offer
{
    public class OfferAddVIewModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int ProjectId { get; set; }

        public ProjectOfferAddViewModel Project { get; set; }

        public string Position { get; set; }

        [Required]
        [Range(0,double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(DataConstants.DescriptionMaxLength, 
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.DescriptionMinLength)]
        public string Description { get; set; }
    }
}
