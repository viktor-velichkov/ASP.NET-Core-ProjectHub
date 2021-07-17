using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{
    public class Contractor : ApplicationUser
    {
        public ICollection<Activity> Activities { get; set; }
    }
}
