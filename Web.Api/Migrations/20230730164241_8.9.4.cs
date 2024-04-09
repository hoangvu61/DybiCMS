using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _894 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyLanguageConfig_Companies_CompanyId",
                table: "CompanyLanguageConfig");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateLanguage_Templates_TemplateName",
                table: "TemplateLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemplateLanguage",
                table: "TemplateLanguage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyLanguageConfig",
                table: "CompanyLanguageConfig");

            migrationBuilder.RenameTable(
                name: "TemplateLanguage",
                newName: "TemplateLanguageKeys");

            migrationBuilder.RenameTable(
                name: "CompanyLanguageConfig",
                newName: "CompanyLanguageConfigs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemplateLanguageKeys",
                table: "TemplateLanguageKeys",
                columns: new[] { "TemplateName", "LanguageKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyLanguageConfigs",
                table: "CompanyLanguageConfigs",
                columns: new[] { "CompanyId", "LanguageKey", "LanguageCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyLanguageConfigs_Companies_CompanyId",
                table: "CompanyLanguageConfigs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateLanguageKeys_Templates_TemplateName",
                table: "TemplateLanguageKeys",
                column: "TemplateName",
                principalTable: "Templates",
                principalColumn: "TemplateName",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyLanguageConfigs_Companies_CompanyId",
                table: "CompanyLanguageConfigs");

            migrationBuilder.DropForeignKey(
                name: "FK_TemplateLanguageKeys_Templates_TemplateName",
                table: "TemplateLanguageKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemplateLanguageKeys",
                table: "TemplateLanguageKeys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyLanguageConfigs",
                table: "CompanyLanguageConfigs");

            migrationBuilder.RenameTable(
                name: "TemplateLanguageKeys",
                newName: "TemplateLanguage");

            migrationBuilder.RenameTable(
                name: "CompanyLanguageConfigs",
                newName: "CompanyLanguageConfig");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemplateLanguage",
                table: "TemplateLanguage",
                columns: new[] { "TemplateName", "LanguageKey" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyLanguageConfig",
                table: "CompanyLanguageConfig",
                columns: new[] { "CompanyId", "LanguageKey", "LanguageCode" });

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyLanguageConfig_Companies_CompanyId",
                table: "CompanyLanguageConfig",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateLanguage_Templates_TemplateName",
                table: "TemplateLanguage",
                column: "TemplateName",
                principalTable: "Templates",
                principalColumn: "TemplateName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
