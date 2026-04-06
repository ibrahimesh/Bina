using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bina.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSeededUserAndProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "CreatedAt", "Email", "FullName", "IsActive", "IsVerified", "LastLoginAt", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { 1, "", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "test@bina.az", "Test ?stifad?çi", true, true, null, "$2a$11$0FfXpB9q.7/aK2YcOoK/A.N/iK1G1M/G8c8bH/Y1h...dummy", "+994501112233", 1 });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "Id", "Area", "CategoryId", "CityId", "CreatedAt", "Description", "DistrictId", "ExpiresAt", "Floor", "HasMortgage", "HasRepair", "IsNegotiable", "IsUrgent", "ListingType", "MetroId", "Price", "RoomCount", "Status", "Title", "TotalFloors", "UpdatedAt", "UserId", "ViewCount" },
                values: new object[,]
                {
                    { 1, 95.0, 1, 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cox göz?l v? i??ql? m?nzildir. ??yalarla birlikd? sat?l?r.", 4, null, 5, false, true, true, true, 1, 4, 185000m, 3, 1, "G?nclik m/s yax?nl???nda 3 otaql? t?mirli m?nzil", 16, null, 1, 15 },
                    { 2, 120.0, 5, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Yol k?nar?nda gür gedi?-g?li?li yerd? yerl??ir.", 7, null, 1, false, true, false, false, 2, null, 2500m, 2, 1, "Nizami rayonunda obyekt icarey? verilir", 5, null, 1, 42 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
