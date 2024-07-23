using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8226 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseInputSources");

            migrationBuilder.DropTable(
                name: "WarehouseOutputOrders");

            migrationBuilder.DropTable(
                name: "WarehouseSources");

            migrationBuilder.CreateTable(
                name: "WarehouseFactorys",
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
                    table.PrimaryKey("PK_WarehouseFactorys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseFactorys_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputToOrders",
                columns: table => new
                {
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputToOrders", x => new { x.OutputId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToOrders_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseSuppliers",
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
                    table.PrimaryKey("PK_WarehouseSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseSuppliers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInputFromFactorys",
                columns: table => new
                {
                    WarehouseInputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FactoryPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FactoryEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FactoryAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputFromFactorys", x => new { x.WarehouseInputId, x.FactoryId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromFactorys_WarehouseFactorys_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "WarehouseFactorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromFactorys_WarehouseInputs_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputToFactorys",
                columns: table => new
                {
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FactoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FactoryPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    FactoryEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FactoryAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputToFactorys", x => new { x.OutputId, x.FactoryId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToFactorys_WarehouseFactorys_FactoryId",
                        column: x => x.FactoryId,
                        principalTable: "WarehouseFactorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToFactorys_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInputFromSuppliers",
                columns: table => new
                {
                    WarehouseInputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SupplierPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SupplierEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupplierAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputFromSuppliers", x => new { x.WarehouseInputId, x.SourceId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromSuppliers_WarehouseInputs_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromSuppliers_WarehouseSuppliers_SourceId",
                        column: x => x.SourceId,
                        principalTable: "WarehouseSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseFactorys_CompanyId",
                table: "WarehouseFactorys",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromFactorys_FactoryId",
                table: "WarehouseInputFromFactorys",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromFactorys_WarehouseId",
                table: "WarehouseInputFromFactorys",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromSuppliers_SourceId",
                table: "WarehouseInputFromSuppliers",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromSuppliers_WarehouseId",
                table: "WarehouseInputFromSuppliers",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToFactorys_FactoryId",
                table: "WarehouseOutputToFactorys",
                column: "FactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToOrders_OrderId",
                table: "WarehouseOutputToOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToOrders_OutputId",
                table: "WarehouseOutputToOrders",
                column: "OutputId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseSuppliers_CompanyId",
                table: "WarehouseSuppliers",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseInputFromFactorys");

            migrationBuilder.DropTable(
                name: "WarehouseInputFromSuppliers");

            migrationBuilder.DropTable(
                name: "WarehouseOutputToFactorys");

            migrationBuilder.DropTable(
                name: "WarehouseOutputToOrders");

            migrationBuilder.DropTable(
                name: "WarehouseSuppliers");

            migrationBuilder.DropTable(
                name: "WarehouseFactorys");

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
                name: "WarehouseSources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
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
                name: "WarehouseInputSources",
                columns: table => new
                {
                    WarehouseInputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    SourceEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SourceName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SourcePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputSources", x => new { x.WarehouseInputId, x.SourceId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputSources_WarehouseInputs_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarehouseInputSources_WarehouseSources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "WarehouseSources",
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
                name: "IX_WarehouseSources_CompanyId",
                table: "WarehouseSources",
                column: "CompanyId");
        }
    }
}
