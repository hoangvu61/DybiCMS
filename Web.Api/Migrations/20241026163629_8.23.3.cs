using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8233 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemProductCodes");

            migrationBuilder.CreateTable(
                name: "ItemProductSeries",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Seri = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProductSeries", x => new { x.ProductId, x.Seri });
                    table.ForeignKey(
                        name: "FK_ItemProductSeries_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemProductSeries");

            migrationBuilder.CreateTable(
                name: "ItemProductCodes",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProductCodes", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ItemProductCodes_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
