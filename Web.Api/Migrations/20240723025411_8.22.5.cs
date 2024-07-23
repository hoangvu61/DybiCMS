using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8225 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseOutputs_WarehouseOutputId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputs_WarehouseSources_SourceId",
                table: "WarehouseInputs");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputs_SourceId",
                table: "WarehouseInputs");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputProducts_WarehouseOutputId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropColumn(
                name: "ToAddress",
                table: "WarehouseOutputs");

            migrationBuilder.DropColumn(
                name: "ToName",
                table: "WarehouseOutputs");

            migrationBuilder.DropColumn(
                name: "ToPhone",
                table: "WarehouseOutputs");

            migrationBuilder.DropColumn(
                name: "SourceId",
                table: "WarehouseInputs");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "WarehouseInputProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseOutputId",
                table: "WarehouseInputProducts");

            migrationBuilder.CreateTable(
                name: "CompanyConfigs",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyConfigs", x => new { x.CompanyId, x.Key });
                    table.ForeignKey(
                        name: "FK_CompanyConfigs_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Configs",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configs", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInputSources",
                columns: table => new
                {
                    WarehouseInputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SourcePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SourceEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SourceAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputSources", x => new { x.WarehouseInputId, x.SourceId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputSources_WarehouseInputs_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseInputSources_WarehouseSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "WarehouseSources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputOrders",
                columns: table => new
                {
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputOrders", x => new { x.OutputId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputOrders_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputProductDetails",
                columns: table => new
                {
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputProductDetails", x => new { x.OutputId, x.InputId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputProductDetails_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputProductDetails_WarehouseInputs_InputId",
                        column: x => x.InputId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputProductDetails_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputProducts",
                columns: table => new
                {
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputProducts", x => new { x.OutputId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputProducts_ItemProducts_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ItemProducts",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputProducts_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputSources_SourceId",
                table: "WarehouseInputSources",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputSources_WarehouseId",
                table: "WarehouseInputSources",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputOrders_OrderId",
                table: "WarehouseOutputOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputOrders_OutputId",
                table: "WarehouseOutputOrders",
                column: "OutputId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductDetails_InputId",
                table: "WarehouseOutputProductDetails",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductDetails_ProductId",
                table: "WarehouseOutputProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProducts_ProductId",
                table: "WarehouseOutputProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyConfigs");

            migrationBuilder.DropTable(
                name: "Configs");

            migrationBuilder.DropTable(
                name: "WarehouseInputSources");

            migrationBuilder.DropTable(
                name: "WarehouseOutputOrders");

            migrationBuilder.DropTable(
                name: "WarehouseOutputProductDetails");

            migrationBuilder.DropTable(
                name: "WarehouseOutputProducts");

            migrationBuilder.AddColumn<string>(
                name: "ToAddress",
                table: "WarehouseOutputs",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToName",
                table: "WarehouseOutputs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToPhone",
                table: "WarehouseOutputs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SourceId",
                table: "WarehouseInputs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "WarehouseInputProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseOutputId",
                table: "WarehouseInputProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputs_SourceId",
                table: "WarehouseInputs",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProducts_WarehouseOutputId",
                table: "WarehouseInputProducts",
                column: "WarehouseOutputId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseOutputs_WarehouseOutputId",
                table: "WarehouseInputProducts",
                column: "WarehouseOutputId",
                principalTable: "WarehouseOutputs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputs_WarehouseSources_SourceId",
                table: "WarehouseInputs",
                column: "SourceId",
                principalTable: "WarehouseSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
