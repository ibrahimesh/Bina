using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bina.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Cox gцz?l v? i??ql? m?nzildir. ??yalarla birlikd? sat?l?r.");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Yol k?nar?nda gьr gedi?-g?li?li yerd? yerl??ir.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Test ?stifad?зi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Cox göz?l v? i??ql? m?nzildir. ??yalarla birlikd? sat?l?r.");

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Yol k?nar?nda gür gedi?-g?li?li yerd? yerl??ir.");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "FullName",
                value: "Test ?stifad?çi");
        }
    }
}
