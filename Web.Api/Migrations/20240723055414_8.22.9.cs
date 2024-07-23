using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8229 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputProductCodes_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputProducts_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseInputProductCodeProductCode",
                table: "WarehouseInputProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WarehouseInputProductCodeProductCode",
                table: "WarehouseInputProducts",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProducts_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts",
                columns: new[] { "WarehouseInputProductCodeProductCode", "WarehouseInputProductCodeProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputProductCodes_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts",
                columns: new[] { "WarehouseInputProductCodeProductCode", "WarehouseInputProductCodeProductId" },
                principalTable: "WarehouseInputProductCodes",
                principalColumns: new[] { "ProductCode", "ProductId" });
        }
    }
}
