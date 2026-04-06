using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bina.DAL.Models;

namespace Bina.DAL.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.HasData(
                new City { Id = 1, Name = "Bak\u0131" },
                new City { Id = 2, Name = "G\u0259nc\u0259" },
                new City { Id = 3, Name = "Sumqay\u0131t" }
            );
        }
    }

    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);

            // Seeding 10 Bakı districts
            builder.HasData(
                new District { Id = 1, Name = "Bin\u0259q\u0259di", CityId = 1 },
                new District { Id = 2, Name = "Yasamal", CityId = 1 },
                new District { Id = 3, Name = "N\u0259simi", CityId = 1 },
                new District { Id = 4, Name = "N\u0259rimanov", CityId = 1 },
                new District { Id = 5, Name = "X\u0259tai", CityId = 1 },
                new District { Id = 6, Name = "S\u0259bail", CityId = 1 },
                new District { Id = 7, Name = "Nizami", CityId = 1 },
                new District { Id = 8, Name = "Suraxan\u0131", CityId = 1 },
                new District { Id = 9, Name = "Sabun\u00E7u", CityId = 1 },
                new District { Id = 10, Name = "Qarada\u011F", CityId = 1 }
            );
        }
    }

    public class MetroConfiguration : IEntityTypeConfiguration<Metro>
    {
        public void Configure(EntityTypeBuilder<Metro> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);

            // 5 metro stations
            builder.HasData(
                new Metro { Id = 1, Name = "28 May", CityId = 1 },
                new Metro { Id = 2, Name = "Sahil", CityId = 1 },
                new Metro { Id = 3, Name = "Elml\u0259r Akademiyas\u0131", CityId = 1 },
                new Metro { Id = 4, Name = "G\u0259nclik", CityId = 1 },
                new Metro { Id = 5, Name = "N\u0259riman N\u0259rimanov", CityId = 1 }
            );
        }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Slug).IsRequired().HasMaxLength(150);
            builder.HasIndex(c => c.Slug).IsUnique();

            builder.HasMany(c => c.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seeding 5 categories (Mənzil, Ev, Torpaq, Ofis, Obyekt)
            builder.HasData(
                new Category { Id = 1, Name = "M\u0259nzil", Slug = "menzil", IconUrl = "" },
                new Category { Id = 2, Name = "Ev", Slug = "ev", IconUrl = "" },
                new Category { Id = 3, Name = "Torpaq", Slug = "torpaq", IconUrl = "" },
                new Category { Id = 4, Name = "Ofis", Slug = "ofis", IconUrl = "" },
                new Category { Id = 5, Name = "Obyekt", Slug = "obyekt", IconUrl = "" }
            );
        }
    }
}