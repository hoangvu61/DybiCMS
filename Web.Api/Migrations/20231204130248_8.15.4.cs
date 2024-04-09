using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8154 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCategory_Attributes_AttributeId_CompanyId",
                table: "AttributeCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCategory_ItemCategories_CategoryId",
                table: "AttributeCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeCategory",
                table: "AttributeCategory");

            migrationBuilder.RenameTable(
                name: "AttributeCategory",
                newName: "AttributeCategories");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeCategory_CategoryId",
                table: "AttributeCategories",
                newName: "IX_AttributeCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeCategories",
                table: "AttributeCategories",
                columns: new[] { "AttributeId", "CompanyId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCategories_Attributes_AttributeId_CompanyId",
                table: "AttributeCategories",
                columns: new[] { "AttributeId", "CompanyId" },
                principalTable: "Attributes",
                principalColumns: new[] { "Id", "CompanyId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCategories_ItemCategories_CategoryId",
                table: "AttributeCategories",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCategories_Attributes_AttributeId_CompanyId",
                table: "AttributeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCategories_ItemCategories_CategoryId",
                table: "AttributeCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeCategories",
                table: "AttributeCategories");

            migrationBuilder.RenameTable(
                name: "AttributeCategories",
                newName: "AttributeCategory");

            migrationBuilder.RenameIndex(
                name: "IX_AttributeCategories_CategoryId",
                table: "AttributeCategory",
                newName: "IX_AttributeCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeCategory",
                table: "AttributeCategory",
                columns: new[] { "AttributeId", "CompanyId", "CategoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCategory_Attributes_AttributeId_CompanyId",
                table: "AttributeCategory",
                columns: new[] { "AttributeId", "CompanyId" },
                principalTable: "Attributes",
                principalColumns: new[] { "Id", "CompanyId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCategory_ItemCategories_CategoryId",
                table: "AttributeCategory",
                column: "CategoryId",
                principalTable: "ItemCategories",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
