using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.api.Migrations
{
    /// <inheritdoc />
    public partial class SKU : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Large",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Medium",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OneSize",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Small",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "XLarge",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "XSmall",
                table: "Products",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Products",
                newName: "Sku");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "XSmall");

            migrationBuilder.RenameColumn(
                name: "Sku",
                table: "Products",
                newName: "Color");

            migrationBuilder.AddColumn<int>(
                name: "Large",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Medium",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "OneSize",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Small",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "XLarge",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
