using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Calligraphy.Data.Migrations
{
    public partial class Init_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "About",
                columns: table => new
                {
                    AboutId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Profession = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    Mission = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_About", x => x.AboutId);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(nullable: true),
                    Postal = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ContractId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinalCost = table.Column<double>(nullable: false),
                    DownPayment = table.Column<double>(nullable: false),
                    DateCommissioned = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    HasSignature = table.Column<bool>(nullable: false),
                    IsFinished = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ContractId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageData = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    QuoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<float>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Materials = table.Column<string>(nullable: true),
                    ApprovalStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.QuoteId);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<int>(nullable: false),
                    StartingRate = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    FormId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: true),
                    ServiceType = table.Column<int>(nullable: false),
                    StartingRate = table.Column<float>(nullable: false),
                    Comments = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    QuoteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.FormId);
                    table.ForeignKey(
                        name: "FK_Forms_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Forms_Quotes_QuoteId",
                        column: x => x.QuoteId,
                        principalTable: "Quotes",
                        principalColumn: "QuoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "About",
                columns: new[] { "AboutId", "Country", "Description", "Email", "Experience", "Language", "Mission", "Name", "Phone", "Profession" },
                values: new object[] { 1, "Canada", "I am a student, worked here blabla", "serena22@email.com", "Phd and masters", "All Languages", "Make that moneeeeeyy ya know", "Serena Tam", "(123)-456-7890", "Calligrapher and Engraver" });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Password", "RefreshToken", "UserName" },
                values: new object[] { 1, "$2a$11$B8wtCONHVIPXtca01LqG..L1fZ2pDPjlnjk6LEM8NJySS9AhARp1e", null, "admin" });

            migrationBuilder.InsertData(
                table: "Forms",
                columns: new[] { "FormId", "Comments", "CreatedDate", "CustomerId", "QuoteId", "ServiceType", "StartingRate" },
                values: new object[,]
                {
                    { 1, "I am a student, worked here blabla", new DateTime(2022, 2, 17, 17, 47, 41, 972, DateTimeKind.Local).AddTicks(4437), null, null, 0, 0f },
                    { 2, "I am a student, worked here blabla", new DateTime(2022, 2, 17, 17, 47, 41, 975, DateTimeKind.Local).AddTicks(1436), null, null, 0, 0f },
                    { 3, "I am a student, worked here blabla", new DateTime(2022, 2, 17, 17, 47, 41, 975, DateTimeKind.Local).AddTicks(1485), null, null, 0, 0f },
                    { 4, "I am a student, worked here blabla", new DateTime(2022, 2, 17, 17, 47, 41, 975, DateTimeKind.Local).AddTicks(1502), null, null, 0, 0f },
                    { 5, "I am a student, worked here blabla", new DateTime(2022, 2, 17, 17, 47, 41, 975, DateTimeKind.Local).AddTicks(1517), null, null, 0, 0f }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "ServiceId", "StartingRate", "TypeName" },
                values: new object[,]
                {
                    { 1, 20f, 1 },
                    { 2, 30f, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_UserName",
                table: "Admins",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_AddressId",
                table: "Customers",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_CustomerId",
                table: "Forms",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_QuoteId",
                table: "Forms",
                column: "QuoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "About");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Quotes");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
