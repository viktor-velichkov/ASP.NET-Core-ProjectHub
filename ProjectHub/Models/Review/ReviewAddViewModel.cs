using ProjectHub.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.Review
{
    public class ReviewAddViewModel
    {
        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int RecipientId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(DataConstants.ContentMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.ContentMinLength)]
        [Display(Name = "Review Content")]
        public string Content { get; set; }
    }
}
