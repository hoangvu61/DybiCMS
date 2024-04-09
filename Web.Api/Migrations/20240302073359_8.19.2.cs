using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8192 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemAttributes",
                table: "ItemAttributes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemAttributes",
                table: "ItemAttributes",
                columns: new[] { "ItemId", "AttributeId", "LanguageCode" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemAttributes",
                table: "ItemAttributes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemAttributes",
                table: "ItemAttributes",
                columns: new[] { "ItemId", "AttributeId" });
        }
    }
}
