using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8155 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Attributes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
