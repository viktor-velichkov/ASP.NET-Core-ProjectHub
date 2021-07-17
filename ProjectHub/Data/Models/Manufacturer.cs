using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectHub.Data.Models
{
    public class Manufacturer : User
    {
        public ICollection<Product> Products { get; set; }
    }
}
