using System;

namespace Bina.BLL.DTOs.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        
        public int PropertyId { get; set; }
        
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderAvatar { get; set; }

        public int ReceiverId { get; set; }
        public string ReceiverName { get; set; }
    }

    public class CreateMessageDto
    {
        public string Content { get; set; }
        public int PropertyId { get; set; }
        public int ReceiverId { get; set; }
    }
}