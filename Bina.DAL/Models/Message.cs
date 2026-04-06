using System;

namespace Bina.DAL.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }

        public int SenderId { get; set; }
        public User Sender { get; set; }

        public int ReceiverId { get; set; }
        public User Receiver { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }
    }
}