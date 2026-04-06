using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bina.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixAzerbaijaniChars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bakı");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Gəncə");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Sumqayıt");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Binəqədi");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Nəsimi");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Nərimanov");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Xətai");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "Səbail");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Suraxanı");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Qaradağ");

            migrationBuilder.UpdateData(
                table: "Metros",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Elmlər Akademiyası");

            migrationBuilder.UpdateData(
                table: "Metros",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Gənclik");

            migrationBuilder.UpdateData(
                table: "Metros",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Nəriman Nərimanov");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bak?");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "G?nc?");

            migrationBuilder.UpdateData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Sumqay?t");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bin?q?di");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "N?simi");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "N?rimanov");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "X?tai");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 6,
                column: "Name",
                value: "S?bail");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 8,
                column: "Name",
                value: "Suraxan?");

            migrationBuilder.UpdateData(
                table: "Districts",
                keyColumn: "Id",
                keyValue: 10,
                column: "Name",
                value: "Qarada?");

            migrationBuilder.UpdateData(
                table: "Metros",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Elml?r Akademiyas?");

            migrationBuilder.UpdateData(
                table: "Metros",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "G?nclik");

            migrationBuilder.UpdateData(
                table: "Metros",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "N?riman N?rimanov");
        }
    }
}
