using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8221 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputs_WarehouseInputId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProducts_Warehouses_WarehouseId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInventories_Warehouses_WarehouseId",
                table: "WarehouseInventories");

            migrationBuilder.DropTable(
                name: "CustomerInfos");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "WarehouseInventories",
                newName: "InputId");

            migrationBuilder.RenameColumn(
                name: "WarehouseInputId",
                table: "WarehouseInputProducts",
                newName: "WarehouseOutputId");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "WarehouseInputProducts",
                newName: "InputId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseInputProducts_WarehouseInputId",
                table: "WarehouseInputProducts",
                newName: "IX_WarehouseInputProducts_WarehouseOutputId");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "Orders",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerPhone",
                table: "Orders",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContactName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CustomerAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInputProductCodes",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputProductCodes", x => new { x.ProductCode, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputProductCodes_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseInputProductCodes_WarehouseInputs_InputId",
                        column: x => x.InputId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ToPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ToAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InfoValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfoTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => new { x.ContactId, x.InfoKey });
                    table.ForeignKey(
                        name: "FK_ContactInfos_Contacts_Id",
                        column: x => x.Id,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputProductCodes",
                columns: table => new
                {
                    ProductCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputProductCodes", x => new { x.ProductCode, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputProductCodes_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputProductCodes_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_Id",
                table: "ContactInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyId",
                table: "Contacts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProductCodes_InputId",
                table: "WarehouseInputProductCodes",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProductCodes_ProductId",
                table: "WarehouseInputProductCodes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductCodes_OutputId",
                table: "WarehouseOutputProductCodes",
                column: "OutputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductCodes_ProductId",
                table: "WarehouseOutputProductCodes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputs_InputId",
                table: "WarehouseInputProducts",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseOutputs_WarehouseOutputId",
                table: "WarehouseInputProducts",
                column: "WarehouseOutputId",
                principalTable: "WarehouseOutputs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInventories_WarehouseInputs_InputId",
                table: "WarehouseInventories",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputs_InputId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseOutputs_WarehouseOutputId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInventories_WarehouseInputs_InputId",
                table: "WarehouseInventories");

            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "WarehouseInputProductCodes");

            migrationBuilder.DropTable(
                name: "WarehouseOutputProductCodes");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "WarehouseOutputs");

            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerPhone",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "InputId",
                table: "WarehouseInventories",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "WarehouseOutputId",
                table: "WarehouseInputProducts",
                newName: "WarehouseInputId");

            migrationBuilder.RenameColumn(
                name: "InputId",
                table: "WarehouseInputProducts",
                newName: "WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseInputProducts_WarehouseOutputId",
                table: "WarehouseInputProducts",
                newName: "IX_WarehouseInputProducts_WarehouseInputId");

            migrationBuilder.CreateTable(
                name: "CustomerInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InfoKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InfoTitle = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InfoValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfos", x => new { x.Id, x.InfoKey });
                    table.ForeignKey(
                        name: "FK_CustomerInfos_Customers_Id",
                        column: x => x.Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputs_WarehouseInputId",
                table: "WarehouseInputProducts",
                column: "WarehouseInputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProducts_Warehouses_WarehouseId",
                table: "WarehouseInputProducts",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInventories_Warehouses_WarehouseId",
                table: "WarehouseInventories",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
