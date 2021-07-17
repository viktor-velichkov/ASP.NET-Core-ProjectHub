using System.Collections.Generic;

namespace ProjectHub.Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<ProjectOffer> ProjectOffers { get; set; }
    }
}
