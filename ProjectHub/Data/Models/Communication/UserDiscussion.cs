using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectHub.Data.Models
{
    public class UserDiscussion
    {

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey(nameof(Discussion))]
        public int DiscussionId { get; set; }

        public Discussion Discussion { get; set; }
    }
}