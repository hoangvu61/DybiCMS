using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82223 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttributeOrders",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOrders", x => new { x.AttributeId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_AttributeOrders_Attributes_AttributeId_AttributeCompanyId",
                        columns: x => new { x.AttributeId, x.AttributeCompanyId },
                        principalTable: "Attributes",
                        principalColumns: new[] { "Id", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOrders_AttributeId_AttributeCompanyId",
                table: "AttributeOrders",
                columns: new[] { "AttributeId", "AttributeCompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOrders_CompanyId",
                table: "AttributeOrders",
                column: "CompanyId");
        }
    }
}
