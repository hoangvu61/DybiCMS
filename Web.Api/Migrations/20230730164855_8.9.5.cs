using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _895 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLanguageConfigs");

            migrationBuilder.DropTable(
                name: "TemplateLanguageKeys");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyLanguageConfigs",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageKey = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    LanguageCode = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLanguageConfigs", x => new { x.CompanyId, x.LanguageKey, x.LanguageCode });
                    table.ForeignKey(
                        name: "FK_CompanyLanguageConfigs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateLanguageKeys",
                columns: table => new
                {
                    TemplateName = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false),
                    LanguageKey = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateLanguageKeys", x => new { x.TemplateName, x.LanguageKey });
                    table.ForeignKey(
                        name: "FK_TemplateLanguageKeys_Templates_TemplateName",
                        column: x => x.TemplateName,
                        principalTable: "Templates",
                        principalColumn: "TemplateName",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
