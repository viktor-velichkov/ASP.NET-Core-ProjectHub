using ProjectHub.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.Projects
{
    public class ProjectAddViewModel
    {
        public int InvestorId { get; set; }

        [Required]
        [StringLength(DataConstants.ProjectNameMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.ProjectNameMinLength)]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(DataConstants.ProjectCityMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.ProjectCityMinLength)]
        public string City { get; set; }

        [Required]
        [StringLength(DataConstants.ProjectAddresMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.ProjectAddresMinLength)]
        public string Address { get; set; }

        [Required]
        public DateTime Deadline { get; set; }

        [Required]
        [StringLength(DataConstants.DescriptionMaxLength,
                      ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
                      MinimumLength = DataConstants.DescriptionMinLength)]
        public string Description { get; set; }
    }
}
