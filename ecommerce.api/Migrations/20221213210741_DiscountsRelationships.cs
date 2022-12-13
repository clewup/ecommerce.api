using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.api.Migrations
{
    /// <inheritdoc />
    public partial class DiscountsRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DiscountId",
                table: "Carts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Carts_DiscountId",
                table: "Carts",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Discounts_DiscountId",
                table: "Carts",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Discounts_DiscountId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_DiscountId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Carts");
        }
    }
}
