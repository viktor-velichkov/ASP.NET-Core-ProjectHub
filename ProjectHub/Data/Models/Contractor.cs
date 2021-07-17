using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{
    public class Contractor : User
    {
        public ICollection<Activity> Activities { get; set; }
    }
}
