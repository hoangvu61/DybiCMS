using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8101 : Migration
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
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ItemAttributes",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemAttributes", x => new { x.ItemId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_ItemAttributes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_ItemCategories_CategoryId",
                table: "Attributes");

            migrationBuilder.DropTable(
                name: "ItemAttributes");

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
    }
}
