using System;
using System.Collections.Generic;
using Bina.DAL.Enums;

namespace Bina.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        
        public UserRole Role { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public ICollection<Property> Properties { get; set; } = new List<Property>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        
        // Explicitly tracking sent messages. 
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
    }
}