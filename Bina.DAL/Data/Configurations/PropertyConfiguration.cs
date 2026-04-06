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
                    Title = "G?nclik m/s yax?nl???nda 3 otaql? t?mirli m?nzil",
                    Description = "Cox göz?l v? i??ql? m?nzildir. ??yalarla birlikd? sat?l?r.",
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
                    CategoryId = 1, // M?nzil
                    CityId = 1, // Bak?
                    DistrictId = 4, // N?rimanov
                    MetroId = 4 // G?nclik
                },
                new Property 
                {
                    Id = 2,
                    Title = "Nizami rayonunda obyekt icarey? verilir",
                    Description = "Yol k?nar?nda gür gedi?-g?li?li yerd? yerl??ir.",
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
                    CityId = 1, // Bak?
                    DistrictId = 7, // Nizami
                    MetroId = null
                }
            );
        }
    }
}