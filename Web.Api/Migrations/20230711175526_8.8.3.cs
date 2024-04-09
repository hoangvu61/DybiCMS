using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _883 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValueLanguages_AttributeValues_AttributeValueId",
                table: "AttributeValueLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValueLanguages",
                table: "AttributeValueLanguages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues",
                column: "Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValueLanguages",
                table: "AttributeValueLanguages",
                columns: new[] { "Code", "LanguageCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValueLanguages_AttributeValues_Code",
                table: "AttributeValueLanguages",
                column: "Code",
                principalTable: "AttributeValues",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeValueLanguages_AttributeValues_Code",
                table: "AttributeValueLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeValueLanguages",
                table: "AttributeValueLanguages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValues",
                table: "AttributeValues",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeValueLanguages",
                table: "AttributeValueLanguages",
                columns: new[] { "AttributeValueId", "LanguageCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeValueLanguages_AttributeValues_AttributeValueId",
                table: "AttributeValueLanguages",
                column: "AttributeValueId",
                principalTable: "AttributeValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
