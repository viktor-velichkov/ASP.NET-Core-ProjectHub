using ProjectHub.Data;
using ProjectHub.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.Offer
{
    public class OfferAddVIewModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public Project Project { get; set; }

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
