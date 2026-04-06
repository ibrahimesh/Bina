using System;
using System.Collections.Generic;
using Bina.DAL.Enums;

namespace Bina.DAL.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
        
        public double Area { get; set; } // m²
        
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public int RoomCount { get; set; }
        
        public bool HasRepair { get; set; }
        public bool HasMortgage { get; set; }
        public bool IsUrgent { get; set; }
        
        public PropertyStatus Status { get; set; }
        public ListingType ListingType { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        
        public int ViewCount { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MetroId { get; set; }

        // Navigation Properties
        public User User { get; set; }
        public Category Category { get; set; }
        public City City { get; set; }
        public District District { get; set; }
        public Metro Metro { get; set; }
        
        public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}