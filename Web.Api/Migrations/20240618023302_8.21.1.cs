using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDeliveries",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeliveryId = table.Column<int>(type: "int", nullable: false),
                    DeliveryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    COD = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryNote = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveries", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderDeliveries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDeliveries");
        }
    }
}
