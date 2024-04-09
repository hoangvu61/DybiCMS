using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _886 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AttributeValues",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AttributeValueId",
                table: "AttributeValueLanguages",
                type: "VARCHAR(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AttributeValues",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "AttributeValueId",
                table: "AttributeValueLanguages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "VARCHAR(50)",
                oldMaxLength: 50);

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
    }
}
