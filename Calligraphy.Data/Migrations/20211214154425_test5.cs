using Microsoft.EntityFrameworkCore.Migrations;

namespace Calligraphy.Data.Migrations
{
    public partial class test5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "Postal",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Postal",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Address");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
