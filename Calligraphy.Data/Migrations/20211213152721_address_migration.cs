using Microsoft.EntityFrameworkCore.Migrations;

namespace Calligraphy.Data.Migrations
{
    public partial class address_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Forms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Forms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Forms",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StreetAddress = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Forms_AddressId",
                table: "Forms",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Address_AddressId",
                table: "Forms",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Address_AddressId",
                table: "Forms");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Forms_AddressId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Forms");
        }
    }
}
