using System.Collections.Generic;

namespace ProjectHub.Data.Models
{
    public class Discussion
    {
        public Discussion()
        {
            this.Participants = new HashSet<UserDiscussion>();
            this.Messages = new HashSet<Message>();
        }
        public int Id { get; set; }

        public ICollection<UserDiscussion> Participants { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}