using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82224 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCategories_Attributes_AttributeId_CompanyId",
                table: "AttributeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeContacts_Attributes_AttributeId_AttributeCompanyId",
                table: "AttributeContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLanguages_Attributes_AttributeId_CompanyId",
                table: "AttributeLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_CompanyId",
                table: "Attributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeContacts",
                table: "AttributeContacts");

            migrationBuilder.DropIndex(
                name: "IX_AttributeContacts_AttributeId_AttributeCompanyId",
                table: "AttributeContacts");

            migrationBuilder.DropIndex(
                name: "IX_AttributeContacts_CompanyId",
                table: "AttributeContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeCategories",
                table: "AttributeCategories");

            migrationBuilder.DropColumn(
                name: "AttributeCompanyId",
                table: "AttributeContacts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes",
                columns: new[] { "CompanyId", "Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeContacts",
                table: "AttributeContacts",
                columns: new[] { "CompanyId", "AttributeId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeCategories",
                table: "AttributeCategories",
                columns: new[] { "CompanyId", "AttributeId", "CategoryId" });

            migrationBuilder.CreateTable(
                name: "AttributeOrders",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOrders", x => new { x.CompanyId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_AttributeOrders_Attributes_CompanyId_AttributeId",
                        columns: x => new { x.CompanyId, x.AttributeId },
                        principalTable: "Attributes",
                        principalColumns: new[] { "CompanyId", "Id" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_AttributeOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrderAttributes",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    LanguageCode = table.Column<string>(type: "VARCHAR(5)", maxLength: 5, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAttributes", x => new { x.OrderId, x.AttributeId, x.LanguageCode });
                    table.ForeignKey(
                        name: "FK_OrderAttributes_Attributes_CompanyId_AttributeId",
                        columns: x => new { x.CompanyId, x.AttributeId },
                        principalTable: "Attributes",
                        principalColumns: new[] { "CompanyId", "Id" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderAttributes_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrderAttributes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeLanguages_CompanyId_AttributeId",
                table: "AttributeLanguages",
                columns: new[] { "CompanyId", "AttributeId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderAttributes_CompanyId_AttributeId",
                table: "OrderAttributes",
                columns: new[] { "CompanyId", "AttributeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCategories_Attributes_CompanyId_AttributeId",
                table: "AttributeCategories",
                columns: new[] { "CompanyId", "AttributeId" },
                principalTable: "Attributes",
                principalColumns: new[] { "CompanyId", "Id" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeContacts_Attributes_CompanyId_AttributeId",
                table: "AttributeContacts",
                columns: new[] { "CompanyId", "AttributeId" },
                principalTable: "Attributes",
                principalColumns: new[] { "CompanyId", "Id" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLanguages_Attributes_CompanyId_AttributeId",
                table: "AttributeLanguages",
                columns: new[] { "CompanyId", "AttributeId" },
                principalTable: "Attributes",
                principalColumns: new[] { "CompanyId", "Id" },
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeCategories_Attributes_CompanyId_AttributeId",
                table: "AttributeCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeContacts_Attributes_CompanyId_AttributeId",
                table: "AttributeContacts");

            migrationBuilder.DropForeignKey(
                name: "FK_AttributeLanguages_Attributes_CompanyId_AttributeId",
                table: "AttributeLanguages");

            migrationBuilder.DropTable(
                name: "AttributeOrders");

            migrationBuilder.DropTable(
                name: "OrderAttributes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_AttributeLanguages_CompanyId_AttributeId",
                table: "AttributeLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeContacts",
                table: "AttributeContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttributeCategories",
                table: "AttributeCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "AttributeCompanyId",
                table: "AttributeContacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attributes",
                table: "Attributes",
                columns: new[] { "Id", "CompanyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeContacts",
                table: "AttributeContacts",
                columns: new[] { "AttributeId", "CompanyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttributeCategories",
                table: "AttributeCategories",
                columns: new[] { "AttributeId", "CompanyId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CompanyId",
                table: "Attributes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeContacts_AttributeId_AttributeCompanyId",
                table: "AttributeContacts",
                columns: new[] { "AttributeId", "AttributeCompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeContacts_CompanyId",
                table: "AttributeContacts",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeCategories_Attributes_AttributeId_CompanyId",
                table: "AttributeCategories",
                columns: new[] { "AttributeId", "CompanyId" },
                principalTable: "Attributes",
                principalColumns: new[] { "Id", "CompanyId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeContacts_Attributes_AttributeId_AttributeCompanyId",
                table: "AttributeContacts",
                columns: new[] { "AttributeId", "AttributeCompanyId" },
                principalTable: "Attributes",
                principalColumns: new[] { "Id", "CompanyId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeLanguages_Attributes_AttributeId_CompanyId",
                table: "AttributeLanguages",
                columns: new[] { "AttributeId", "CompanyId" },
                principalTable: "Attributes",
                principalColumns: new[] { "Id", "CompanyId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
