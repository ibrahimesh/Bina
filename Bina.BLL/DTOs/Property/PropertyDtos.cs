using System;
using System.Collections.Generic;
using Bina.BLL.DTOs.Common;

namespace Bina.BLL.DTOs.Property
{
    public class CreatePropertyDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsNegotiable { get; set; }
        public double Area { get; set; }
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public int RoomCount { get; set; }
        public bool HasRepair { get; set; }
        public bool HasMortgage { get; set; }
        public bool IsUrgent { get; set; }
        
        public int CategoryId { get; set; }
        public int CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MetroId { get; set; }
        public int ListingType { get; set; } // Enums cast
    }

    public class UpdatePropertyDto : CreatePropertyDto { }

    public class PropertyListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public double Area { get; set; }
        public int RoomCount { get; set; }
        public string MainImageUrl { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsFavorite { get; set; }
    }

    public class PropertyResponseDto : PropertyListDto
    {
        public string Description { get; set; }
        public bool IsNegotiable { get; set; }
        public int Floor { get; set; }
        public int TotalFloors { get; set; }
        public bool HasRepair { get; set; }
        public bool HasMortgage { get; set; }
        public bool IsUrgent { get; set; }
        public string MetroName { get; set; }
        public string CategoryName { get; set; }
        
        public List<string> ImageUrls { get; set; } = new List<string>();
    }

    public class PropertyFilterDto
    {
        public int? CategoryId { get; set; }
        public int? CityId { get; set; }
        public int? DistrictId { get; set; }
        public int? MetroId { get; set; }
        public int? ListingType { get; set; }
        
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public double? MinArea { get; set; }
        public double? MaxArea { get; set; }
        public int? RoomCount { get; set; }
        public bool? HasRepair { get; set; }
        public bool? HasMortgage { get; set; }
        public bool? IsUrgent { get; set; }
        public string Keyword { get; set; }
        
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public string SortBy { get; set; } = "date_desc"; 
    }
}