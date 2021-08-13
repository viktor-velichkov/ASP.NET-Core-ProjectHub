﻿using ProjectHub.Data.Models.Projects;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Designer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Discipline))]
        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public int? WorkExperience { get; set; }

        public ICollection<ProjectDesigner> Projects => new HashSet<ProjectDesigner>();
    }
}
