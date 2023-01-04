using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ecommerce.api.Migrations
{
    /// <inheritdoc />
    public partial class Shipping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TrackingNumber",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrackingNumber = table.Column<Guid>(type: "uuid", nullable: false),
                    ShippedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: false),
                    AddedBy = table.Column<string>(type: "text", nullable: false),
                    AddedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TrackingNumber",
                table: "Orders",
                column: "TrackingNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Packages_TrackingNumber",
                table: "Orders",
                column: "TrackingNumber",
                principalTable: "Packages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Packages_TrackingNumber",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TrackingNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "Orders");
        }
    }
}
