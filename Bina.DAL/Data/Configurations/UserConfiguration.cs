using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bina.DAL.Models;

namespace Bina.DAL.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(150);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(150);
            
            builder.HasMany(u => u.SentMessages)
                   .WithOne(m => m.Sender)
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.ReceivedMessages)
                   .WithOne(m => m.Receiver)
                   .HasForeignKey(m => m.ReceiverId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Favorites)
                   .WithOne(f => f.User)
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                new User 
                { 
                    Id = 1, 
                    FullName = "Test ?stifad?çi", 
                    Email = "test@bina.az", 
                    PhoneNumber = "+994501112233", 
                    PasswordHash = "$2a$11$0FfXpB9q.7/aK2YcOoK/A.N/iK1G1M/G8c8bH/Y1h...dummy", // Dummy hash
                    Role = Enums.UserRole.User,
                    AvatarUrl = "",
                    IsVerified = true,
                    IsActive = true,
                    CreatedAt = new System.DateTime(2024, 1, 1, 0, 0, 0, System.DateTimeKind.Utc)
                }
            );
        }
    }
}