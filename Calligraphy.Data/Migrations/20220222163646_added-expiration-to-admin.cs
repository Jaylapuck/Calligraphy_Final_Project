using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calligraphy.Data.Migrations
{
    public partial class addedexpirationtoadmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpirationDate",
                table: "Admins",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$xK2LgcZllzzz6cHzcZ0WNu6a7Up7UYz5V6noa2CU.MJIhKrs..Pq2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokenExpirationDate",
                table: "Admins");

            migrationBuilder.UpdateData(
                table: "Admins",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$XEVQfPPtoWqWOgS8PG4n3uhGY0Vwg85HbWQCOyyZNgjOzqboxQAL6");
        }
    }
}
