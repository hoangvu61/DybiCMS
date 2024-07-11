using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8215 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleMin",
                table: "ItemProducts");

            migrationBuilder.CreateTable(
                name: "AttributeContacts",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeContacts", x => new { x.AttributeId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_AttributeContacts_Attributes_AttributeId_AttributeCompanyId",
                        columns: x => new { x.AttributeId, x.AttributeCompanyId },
                        principalTable: "Attributes",
                        principalColumns: new[] { "Id", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeContacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AttributeOrders",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeCompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeOrders", x => new { x.AttributeId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_AttributeOrders_Attributes_AttributeId_AttributeCompanyId",
                        columns: x => new { x.AttributeId, x.AttributeCompanyId },
                        principalTable: "Attributes",
                        principalColumns: new[] { "Id", "CompanyId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseSources_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInventories",
                columns: table => new
                {
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInventories", x => new { x.WarehouseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseInventories_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseInventories_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseInputs_WarehouseSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "WarehouseSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseInputs_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInputProducts",
                columns: table => new
                {
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    WarehouseInputId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputProducts", x => new { x.WarehouseId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputProducts_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseInputProducts_WarehouseInputs_WarehouseInputId",
                        column: x => x.WarehouseInputId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseInputProducts_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeContacts_AttributeId_AttributeCompanyId",
                table: "AttributeContacts",
                columns: new[] { "AttributeId", "AttributeCompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeContacts_CompanyId",
                table: "AttributeContacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOrders_AttributeId_AttributeCompanyId",
                table: "AttributeOrders",
                columns: new[] { "AttributeId", "AttributeCompanyId" });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeOrders_CompanyId",
                table: "AttributeOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProducts_ProductId",
                table: "WarehouseInputProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProducts_WarehouseInputId",
                table: "WarehouseInputProducts",
                column: "WarehouseInputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputs_SourceId",
                table: "WarehouseInputs",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputs_WarehouseId",
                table: "WarehouseInputs",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInventories_ProductId",
                table: "WarehouseInventories",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CompanyId",
                table: "Warehouses",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseSources_CompanyId",
                table: "WarehouseSources",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeContacts");

            migrationBuilder.DropTable(
                name: "AttributeOrders");

            migrationBuilder.DropTable(
                name: "WarehouseInputProducts");

            migrationBuilder.DropTable(
                name: "WarehouseInventories");

            migrationBuilder.DropTable(
                name: "WarehouseInputs");

            migrationBuilder.DropTable(
                name: "WarehouseSources");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "SaleMin",
                table: "ItemProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
