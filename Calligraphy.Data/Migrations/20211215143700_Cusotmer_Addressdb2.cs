using Microsoft.EntityFrameworkCore.Migrations;

namespace Calligraphy.Data.Migrations
{
    public partial class Cusotmer_Addressdb2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Customers_CustomerId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_CustomerId",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AddressId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerEntity",
                table: "Forms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressEntity",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_CustomerEntity",
                table: "Forms",
                column: "CustomerEntity");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressEntity",
                table: "Customers",
                column: "AddressEntity");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressEntity",
                table: "Customers",
                column: "AddressEntity",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Customers_CustomerEntity",
                table: "Forms",
                column: "CustomerEntity",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Addresses_AddressEntity",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Customers_CustomerEntity",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Forms_CustomerEntity",
                table: "Forms");

            migrationBuilder.DropIndex(
                name: "IX_Customers_AddressEntity",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerEntity",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "AddressEntity",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Forms",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Forms_CustomerId",
                table: "Forms",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Addresses_AddressId",
                table: "Customers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "AddressId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Customers_CustomerId",
                table: "Forms",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
