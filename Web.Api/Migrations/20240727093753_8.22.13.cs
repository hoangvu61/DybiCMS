using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82213 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseFactoryId",
                table: "WarehouseOutputs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseFactoryId",
                table: "WarehouseInputs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseSupplierId",
                table: "WarehouseInputs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DebtCustomers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtCustomers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebtCustomers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DebtSuppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalDebt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtSuppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DebtSuppliers_WarehouseSuppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "WarehouseSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDebts",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitExpire = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDebts", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderDebts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseInputDebts",
                columns: table => new
                {
                    InputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Debit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitExpire = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputDebts", x => x.InputId);
                    table.ForeignKey(
                        name: "FK_WarehouseInputDebts_WarehouseInputs_InputId",
                        column: x => x.InputId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputs_WarehouseFactoryId",
                table: "WarehouseOutputs",
                column: "WarehouseFactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputs_WarehouseFactoryId",
                table: "WarehouseInputs",
                column: "WarehouseFactoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputs_WarehouseSupplierId",
                table: "WarehouseInputs",
                column: "WarehouseSupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_DebtCustomers_CustomerId",
                table: "DebtCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DebtSuppliers_SupplierId",
                table: "DebtSuppliers",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputs_WarehouseFactorys_WarehouseFactoryId",
                table: "WarehouseInputs",
                column: "WarehouseFactoryId",
                principalTable: "WarehouseFactorys",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputs_WarehouseSuppliers_WarehouseSupplierId",
                table: "WarehouseInputs",
                column: "WarehouseSupplierId",
                principalTable: "WarehouseSuppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputs_WarehouseFactorys_WarehouseFactoryId",
                table: "WarehouseOutputs",
                column: "WarehouseFactoryId",
                principalTable: "WarehouseFactorys",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputs_WarehouseFactorys_WarehouseFactoryId",
                table: "WarehouseInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputs_WarehouseSuppliers_WarehouseSupplierId",
                table: "WarehouseInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputs_WarehouseFactorys_WarehouseFactoryId",
                table: "WarehouseOutputs");

            migrationBuilder.DropTable(
                name: "DebtCustomers");

            migrationBuilder.DropTable(
                name: "DebtSuppliers");

            migrationBuilder.DropTable(
                name: "OrderDebts");

            migrationBuilder.DropTable(
                name: "WarehouseInputDebts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputs_WarehouseFactoryId",
                table: "WarehouseOutputs");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputs_WarehouseFactoryId",
                table: "WarehouseInputs");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputs_WarehouseSupplierId",
                table: "WarehouseInputs");

            migrationBuilder.DropColumn(
                name: "WarehouseFactoryId",
                table: "WarehouseOutputs");

            migrationBuilder.DropColumn(
                name: "WarehouseFactoryId",
                table: "WarehouseInputs");

            migrationBuilder.DropColumn(
                name: "WarehouseSupplierId",
                table: "WarehouseInputs");
        }
    }
}
