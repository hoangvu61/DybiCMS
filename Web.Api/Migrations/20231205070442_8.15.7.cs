using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8157 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CategoryId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Attributes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Attributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CategoryId",
                table: "Attributes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "ItemId");
        }
    }
}
