using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bina.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixEncodingAndQueryIncludes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Çox gözəl və işıqlı mənzildir. Əşyalarla birlikdə satılır.", "Gənclik m/s yaxınlığında 3 otaqlı təmirli mənzil" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Yol kənarında gür gediş-gəlişli yerdə yerləşir.", "Nizami rayonunda obyekt icarəyə verilir" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Cox gцz?l v? i??ql? m?nzildir. ??yalarla birlikd? sat?l?r.", "G?nclik m/s yax?nl???nda 3 otaql? t?mirli m?nzil" });

            migrationBuilder.UpdateData(
                table: "Properties",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Yol k?nar?nda gьr gedi?-g?li?li yerd? yerl??ir.", "Nizami rayonunda obyekt icarey? verilir" });
        }
    }
}
