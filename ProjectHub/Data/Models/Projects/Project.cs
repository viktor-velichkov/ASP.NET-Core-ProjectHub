using ProjectHub.Data.Models.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Project
    {
        public Project()
        {
            this.Designers = new HashSet<ProjectDesigner>();
            this.Offers = new HashSet<Offer>();
        }
        public int Id { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [MaxLength(DataConstants.ProjectNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.ProjectCityMaxLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(DataConstants.ProjectAddresMaxLength)]
        public string Address { get; set; }
                
        public DateTime Deadline { get; set; }

        [Required]
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public int InvestorId { get; set; }
        public Investor Investor { get; set; }

        [ForeignKey(nameof(Manager))]
        public int? ManagerId { get; set; }
        public Manager Manager { get; set; }

        public ICollection<ProjectDesigner> Designers { get; set; }

        [ForeignKey(nameof(Contractor))]
        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}
