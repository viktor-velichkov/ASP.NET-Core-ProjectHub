using ProjectHub.Data.Models.Projects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.ProjectNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DataConstants.ProjectAddresMaxLength)]
        public string Address { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Budget { get; set; }


        public DateTime? StartingDate { get; set; }

        public TimeSpan ExecutionTime { get; set; }

        public DateTime? Deadline => this.StartingDate != null ? this.StartingDate + this.ExecutionTime : null;

        [Required]
        [MaxLength(DataConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public bool IsFinnished { get; set; }

        public ICollection<ProjectInvestor> Investors => new HashSet<ProjectInvestor>();

        [ForeignKey(nameof(Manager))]
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }

        public ICollection<ProjectDesigner> Designers => new HashSet<ProjectDesigner>();

        public ICollection<ProjectContractor> Contractors => new HashSet<ProjectContractor>();

        public ICollection<ProjectPosition> FreePositions => new HashSet<ProjectPosition>();

        public ICollection<Offer> Offers => new HashSet<Offer>();
    }
}
