using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8235 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "DiscountType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ItemProductSeries",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ItemProductSeries");
        }
    }
}
