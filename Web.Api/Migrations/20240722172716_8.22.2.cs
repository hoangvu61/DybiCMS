using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8222 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInfos",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "InfoKey",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "InfoTitle",
                table: "ContactInfos");

            migrationBuilder.RenameColumn(
                name: "InfoValue",
                table: "ContactInfos",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "AttributeId",
                table: "ContactInfos",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInfos",
                table: "ContactInfos",
                columns: new[] { "ContactId", "AttributeId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactInfos",
                table: "ContactInfos");

            migrationBuilder.DropColumn(
                name: "AttributeId",
                table: "ContactInfos");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ContactInfos",
                newName: "InfoValue");

            migrationBuilder.AddColumn<string>(
                name: "InfoKey",
                table: "ContactInfos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InfoTitle",
                table: "ContactInfos",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactInfos",
                table: "ContactInfos",
                columns: new[] { "ContactId", "InfoKey" });
        }
    }
}
