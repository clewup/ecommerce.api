using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.api.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedModelsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "DiscountedPrice",
                table: "Products",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subcategory",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TotalSavings",
                table: "Products",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DiscountedTotal",
                table: "Orders",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalSavings",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DiscountedTotal",
                table: "Carts",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TotalSavings",
                table: "Carts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Subcategory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TotalSavings",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DiscountedTotal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalSavings",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountedTotal",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "TotalSavings",
                table: "Carts");
        }
    }
}
