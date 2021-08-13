using System.Collections.Generic;

namespace ProjectHub.Data.Models
{
    public class Discussion
    {
        public int Id { get; set; }

        public ICollection<UserDiscussion> Participants { get; set; }
        
        public ICollection<Message> Messages { get; set; }
    }
}