using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82219 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseInputFromOrders",
                columns: table => new
                {
                    WarehouseInputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputFromOrders", x => new { x.WarehouseInputId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromOrders_WarehouseInputs_WarehouseInputId",
                        column: x => x.WarehouseInputId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromOrders_OrderId",
                table: "WarehouseInputFromOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromOrders_WarehouseInputId",
                table: "WarehouseInputFromOrders",
                column: "WarehouseInputId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseInputFromOrders");
        }
    }
}
