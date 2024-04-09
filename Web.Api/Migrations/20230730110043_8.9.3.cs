using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _893 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyLanguageConfig",
                columns: table => new
                {
                    LanguageCode = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageKey = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLanguageConfig", x => new { x.CompanyId, x.LanguageKey, x.LanguageCode });
                    table.ForeignKey(
                        name: "FK_CompanyLanguageConfig_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateLanguage",
                columns: table => new
                {
                    LanguageKey = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    TemplateName = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateLanguage", x => new { x.TemplateName, x.LanguageKey });
                    table.ForeignKey(
                        name: "FK_TemplateLanguage_Templates_TemplateName",
                        column: x => x.TemplateName,
                        principalTable: "Templates",
                        principalColumn: "TemplateName",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLanguageConfig");

            migrationBuilder.DropTable(
                name: "TemplateLanguage");
        }
    }
}
