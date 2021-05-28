using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RenielDavidInventorySystem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Tagline = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Tagline = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: true),
                    CategoryID = table.Column<Guid>(nullable: true),
                    TagId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductTags_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "Brand", "CategoryId", "CreatedAt", "Description", "Name", "Price", "Tagline", "UpdatedAt" },
                values: new object[] { new Guid("6ffe01b3-4f05-421e-a35f-4dda3a7f0fe2"), "Emperador", new Guid("863317ad-ec02-47db-97cb-ad6cfe0e28d8"), new DateTime(2021, 5, 28, 10, 39, 18, 605, DateTimeKind.Utc).AddTicks(4164), "Lights", "Brandy", 0m, "PALASING", new DateTime(2021, 5, 28, 10, 39, 18, 605, DateTimeKind.Utc).AddTicks(4165) });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "ID", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("810203f8-e1c2-45f1-b411-f6f97d00ce01"), new DateTime(2021, 5, 28, 10, 39, 18, 602, DateTimeKind.Utc).AddTicks(2592), "Tag 1", new DateTime(2021, 5, 28, 10, 39, 18, 602, DateTimeKind.Utc).AddTicks(3041) },
                    { new Guid("810203f8-e1c2-45f1-b411-f6f97d00ce02"), new DateTime(2021, 5, 28, 10, 39, 18, 603, DateTimeKind.Utc).AddTicks(655), "Tag 2", new DateTime(2021, 5, 28, 10, 39, 18, 603, DateTimeKind.Utc).AddTicks(676) },
                    { new Guid("810203f8-e1c2-45f1-b411-f6f97d00ce03"), new DateTime(2021, 5, 28, 10, 39, 18, 603, DateTimeKind.Utc).AddTicks(718), "Tag 3", new DateTime(2021, 5, 28, 10, 39, 18, 603, DateTimeKind.Utc).AddTicks(719) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_CategoryID",
                table: "ProductTags",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagId",
                table: "ProductTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
