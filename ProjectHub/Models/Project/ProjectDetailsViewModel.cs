using System;
using System.Collections.Generic;

namespace ProjectHub.Models.Project
{
    public class ProjectDetailsViewModel
    {
        public int Id { get; set; }
                
        public string Name { get; set; }
        
        public string City { get; set; }
        
        public string Address { get; set; }

        public string Deadline { get; set; }

        public string Description { get; set; }
        
        public string Investor { get; set; }
        
        public string Manager { get; set; }
        public ICollection<string> Designers => new HashSet<string>();
        
        public string Contractor { get; set; }        
    }
}
