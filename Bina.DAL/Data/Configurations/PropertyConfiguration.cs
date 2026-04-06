using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bina.DAL.Models;

namespace Bina.DAL.Data.Configurations
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);

            builder.HasIndex(p => p.UserId);
            builder.HasIndex(p => p.CityId);
            builder.HasIndex(p => p.CategoryId);
            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => p.ListingType);

            builder.HasData(
                new Property 
                {
                    Id = 1,
                    Title = "G\u0259nclik m/s yax\u0131nl\u0131\u011F\u0131nda 3 otaql\u0131 t\u0259mirli m\u0259nzil",
                    Description = "\u00C7ox g\u00F6z\u0259l v\u0259 i\u015F\u0131ql\u0131 m\u0259nzildir. \u018F\u015Fyalarla birlikd\u0259 sat\u0131l\u0131r.",
                    Price = 185000,
                    IsNegotiable = true,
                    Area = 95,
                    Floor = 5,
                    TotalFloors = 16,
                    RoomCount = 3,
                    HasRepair = true,
                    HasMortgage = false,
                    IsUrgent = true,
                    Status = Enums.PropertyStatus.Active,
                    ListingType = Enums.ListingType.ForSale,
                    CreatedAt = new System.DateTime(2024, 1, 1, 0, 0, 0, System.DateTimeKind.Utc),
                    ViewCount = 15,
                    UserId = 1,
                    CategoryId = 1, // M\u0259nzil
                    CityId = 1, // Bak\u0131
                    DistrictId = 4, // N\u0259rimanov
                    MetroId = 4 // G\u0259nclik
                },
                new Property 
                {
                    Id = 2,
                    Title = "Nizami rayonunda obyekt icar\u0259y\u0259 verilir",
                    Description = "Yol k\u0259nar\u0131nda g\u00FCr gedi\u015F-g\u0259li\u015Fli yerd\u0259 yerl\u0259\u015Fir.",
                    Price = 2500,
                    IsNegotiable = false,
                    Area = 120,
                    Floor = 1,
                    TotalFloors = 5,
                    RoomCount = 2,
                    HasRepair = true,
                    HasMortgage = false,
                    IsUrgent = false,
                    Status = Enums.PropertyStatus.Active,
                    ListingType = Enums.ListingType.ForRent,
                    CreatedAt = new System.DateTime(2024, 1, 2, 0, 0, 0, System.DateTimeKind.Utc),
                    ViewCount = 42,
                    UserId = 1,
                    CategoryId = 5, // Obyekt
                    CityId = 1, // Bak\u0131
                    DistrictId = 7, // Nizami
                    MetroId = null
                }
            );
        }
    }
}