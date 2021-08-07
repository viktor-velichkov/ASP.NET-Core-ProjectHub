using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class Review
    {
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }

        public ApplicationUser Author { get; set; }


        [ForeignKey(nameof(Recipient))]
        public int RecipientId { get; set; }

        public ApplicationUser Recipient { get; set; }

        public DateTime Date { get; set; }

        [Required]
        [MaxLength(DataConstants.ContentMaxLength)]
        public string Content { get; set; }

    }
}