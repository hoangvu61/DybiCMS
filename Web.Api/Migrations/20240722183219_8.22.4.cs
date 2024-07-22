using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8224 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInventories_WarehouseInputs_InputId",
                table: "WarehouseInventories");

            migrationBuilder.RenameColumn(
                name: "InputId",
                table: "WarehouseInventories",
                newName: "WarehouseId");

            migrationBuilder.CreateTable(
                name: "WarehouseInputInventories",
                columns: table => new
                {
                    InputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputInventories", x => new { x.InputId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputInventories_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseInputInventories_WarehouseInputs_InputId",
                        column: x => x.InputId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputInventories_ProductId",
                table: "WarehouseInputInventories",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInventories_Warehouses_WarehouseId",
                table: "WarehouseInventories",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInventories_Warehouses_WarehouseId",
                table: "WarehouseInventories");

            migrationBuilder.DropTable(
                name: "WarehouseInputInventories");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "WarehouseInventories",
                newName: "InputId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInventories_WarehouseInputs_InputId",
                table: "WarehouseInventories",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
