using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bina.DAL.Models;

namespace Bina.DAL.Data.Configurations
{
    public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.LanguageCode).IsRequired().HasMaxLength(5); // e.g., "en", "az-Latn"
            builder.Property(t => t.EntityType).IsRequired().HasMaxLength(50);
            builder.Property(t => t.PropertyName).IsRequired().HasMaxLength(50);
            builder.Property(t => t.Value).IsRequired();

            builder.HasIndex(t => new { t.EntityType, t.EntityId, t.PropertyName, t.LanguageCode }).IsUnique();
        }
    }
}