using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Rate
    {        
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        
        public int RecipientId { get; set; }

        public ApplicationUser Recipient { get; set; }

        public bool IsPositive { get; set; }
    }
}