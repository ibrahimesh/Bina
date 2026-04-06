using System;

namespace Bina.DAL.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }

        public int PropertyId { get; set; }
        public Property Property { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}