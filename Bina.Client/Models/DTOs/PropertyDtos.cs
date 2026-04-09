using System;

namespace Bina.Client.Models.DTOs
{
    public class PropertyListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Area { get; set; }
        public int RoomCount { get; set; }
        public string MainImageUrl { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public string DistrictName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PropertyResponseDto : PropertyListDto
    {
        public string Description { get; set; } = string.Empty;
        public bool IsNegotiable { get; set; }
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public bool HasRepair { get; set; }
        public bool HasMortgage { get; set; }
        public bool IsUrgent { get; set; }
        public string MetroName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public List<string> ImageUrls { get; set; } = new();
    }

    public class CreatePropertyDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Area { get; set; }
        public int RoomCount { get; set; }
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public int ListingType { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MetroId { get; set; }
        public bool IsNegotiable { get; set; }
        public bool HasRepair { get; set; }
        public bool HasMortgage { get; set; }
        public bool IsUrgent { get; set; }
    }
}
