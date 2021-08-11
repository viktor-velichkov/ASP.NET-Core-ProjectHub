using ProjectHub.Data.Models;
using ProjectHub.Models.Offer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Models.Projects
{
    public class ProjectOffersListViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int InvestorId { get; set; }

        [Display(Name="Position")]
        public string PositionFilter { get; set; }

        [Display(Name = "Discipline")]
        public string DisciplineFilter { get; set; }

        public ICollection<OfferListViewModel> Offers { get; set; }
        public IEnumerable<Discipline> Disciplines { get; set; }
    }
}
