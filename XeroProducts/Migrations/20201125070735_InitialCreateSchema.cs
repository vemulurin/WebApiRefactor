using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XeroProducts.API.Migrations
{
    public partial class InitialCreateSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "varchar(36)", nullable: false),
                    ProductId = table.Column<Guid>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(9)", nullable: true),
                    Description = table.Column<string>(type: "varchar(23)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "varchar(36)", nullable: false),
                    Name = table.Column<string>(type: "varchar(17)", nullable: true),
                    Description = table.Column<string>(type: "varchar(35)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DeliveryPrice = table.Column<decimal>(type: "decimal(4,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
