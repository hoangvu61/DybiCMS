using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _881 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLanguages_Attributes_AttributeId_AttributeCompanyId",
                table: "AttributeLanguages");

            migrationBuilder.DropIndex(
                name: "IX_AttributeLanguages_AttributeId_AttributeCompanyId",
                table: "AttributeLanguages");

            migrationBuilder.DropColumn(
                name: "AttributeCompanyId",
                table: "AttributeLanguages");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLanguages_Attributes_AttributeId_CompanyId",
                table: "AttributeLanguages",
                columns: new[] { "AttributeId", "CompanyId" },
                principalTable: "Attributes",
                principalColumns: new[] { "Id", "CompanyId" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLanguages_Attributes_AttributeId_CompanyId",
                table: "AttributeLanguages");

            migrationBuilder.AddColumn<Guid>(
                name: "AttributeCompanyId",
                table: "AttributeLanguages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AttributeLanguages_AttributeId_AttributeCompanyId",
                table: "AttributeLanguages",
                columns: new[] { "AttributeId", "AttributeCompanyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLanguages_Attributes_AttributeId_AttributeCompanyId",
                table: "AttributeLanguages",
                columns: new[] { "AttributeId", "AttributeCompanyId" },
                principalTable: "Attributes",
                principalColumns: new[] { "Id", "CompanyId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
