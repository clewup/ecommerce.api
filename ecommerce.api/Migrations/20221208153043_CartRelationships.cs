using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.api.Migrations
{
    /// <inheritdoc />
    public partial class CartRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartEntityId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartEntityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartEntityId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "CartEntityProductEntity",
                columns: table => new
                {
                    CartsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartEntityProductEntity", x => new { x.CartsId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CartEntityProductEntity_Carts_CartsId",
                        column: x => x.CartsId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartEntityProductEntity_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartEntityProductEntity_ProductsId",
                table: "CartEntityProductEntity",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartEntityProductEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "CartEntityId",
                table: "Products",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartEntityId",
                table: "Products",
                column: "CartEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartEntityId",
                table: "Products",
                column: "CartEntityId",
                principalTable: "Carts",
                principalColumn: "Id");
        }
    }
}
