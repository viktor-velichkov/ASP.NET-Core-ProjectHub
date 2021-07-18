using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Rate
    {
        public int Id { get; set; }

        
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        
        [ForeignKey(nameof(Recipient))]
        public int RecipientId { get; set; }

        public ApplicationUser Recipient { get; set; }

        public bool IsPositive { get; set; }
    }
}