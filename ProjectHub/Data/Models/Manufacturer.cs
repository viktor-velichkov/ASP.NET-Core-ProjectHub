using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{
    public class Manufacturer : ApplicationUser
    {
        public ICollection<Product> Products { get; set; }
    }
}
