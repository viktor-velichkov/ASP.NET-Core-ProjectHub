using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{

    public class Offer
    {
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        public string Position { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }
    }
}