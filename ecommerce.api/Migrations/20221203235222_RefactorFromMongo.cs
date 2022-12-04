using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.api.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFromMongo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_DiscountCodeModel_DiscountCodeCode",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserModel_UserId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DiscountCodeModel");

            migrationBuilder.DropTable(
                name: "StockModel");

            migrationBuilder.DropTable(
                name: "UserModel");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CartId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Carts_DiscountCodeCode",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "IsDiscounted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsShipped",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DiscountCodeCode",
                table: "Carts",
                newName: "DiscountCode");

            migrationBuilder.RenameColumn(
                name: "isDiscounted",
                table: "CartItemModel",
                newName: "IsDiscounted");

            migrationBuilder.RenameColumn(
                name: "Variant",
                table: "CartItemModel",
                newName: "Description");

            migrationBuilder.AddColumn<int>(
                name: "StockCount",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "CartItemModel",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<string>>(
                name: "Images",
                table: "CartItemModel",
                type: "text[]",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "CartItemModel");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "CartItemModel");

            migrationBuilder.RenameColumn(
                name: "DiscountCode",
                table: "Carts",
                newName: "DiscountCodeCode");

            migrationBuilder.RenameColumn(
                name: "IsDiscounted",
                table: "CartItemModel",
                newName: "isDiscounted");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "CartItemModel",
                newName: "Variant");

            migrationBuilder.AddColumn<bool>(
                name: "IsDiscounted",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShipped",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "DiscountCodeModel",
                columns: table => new
                {
                    Code = table.Column<string>(type: "text", nullable: false),
                    PercentOff = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodeModel", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "StockModel",
                columns: table => new
                {
                    Variant = table.Column<string>(type: "text", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    ProductEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockModel", x => x.Variant);
                    table.ForeignKey(
                        name: "FK_StockModel_Products_ProductEntityId",
                        column: x => x.ProductEntityId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    County = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    LineOne = table.Column<string>(type: "text", nullable: false),
                    LineThree = table.Column<string>(type: "text", nullable: false),
                    LineTwo = table.Column<string>(type: "text", nullable: false),
                    Postcode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CartId",
                table: "Orders",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_DiscountCodeCode",
                table: "Carts",
                column: "DiscountCodeCode");

            migrationBuilder.CreateIndex(
                name: "IX_StockModel_ProductEntityId",
                table: "StockModel",
                column: "ProductEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_DiscountCodeModel_DiscountCodeCode",
                table: "Carts",
                column: "DiscountCodeCode",
                principalTable: "DiscountCodeModel",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carts_CartId",
                table: "Orders",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserModel_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
