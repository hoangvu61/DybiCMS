using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82218 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromFactorys_WarehouseFactorys_FactoryId",
                table: "WarehouseInputFromFactorys");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromFactorys_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromFactorys");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputToFactorys_WarehouseFactorys_FactoryId",
                table: "WarehouseOutputToFactorys");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputToFactorys_WarehouseOutputs_OutputId",
                table: "WarehouseOutputToFactorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseOutputToFactorys",
                table: "WarehouseOutputToFactorys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromFactorys",
                table: "WarehouseInputFromFactorys");

            migrationBuilder.RenameTable(
                name: "WarehouseOutputToFactorys",
                newName: "WarehouseOutputToFactories");

            migrationBuilder.RenameTable(
                name: "WarehouseInputFromFactorys",
                newName: "WarehouseInputFromFactories");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseOutputToFactorys_OutputId",
                table: "WarehouseOutputToFactories",
                newName: "IX_WarehouseOutputToFactories_OutputId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseOutputToFactorys_FactoryId",
                table: "WarehouseOutputToFactories",
                newName: "IX_WarehouseOutputToFactories_FactoryId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseInputFromFactorys_WarehouseId",
                table: "WarehouseInputFromFactories",
                newName: "IX_WarehouseInputFromFactories_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseInputFromFactorys_FactoryId",
                table: "WarehouseInputFromFactories",
                newName: "IX_WarehouseInputFromFactories_FactoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseOutputToFactories",
                table: "WarehouseOutputToFactories",
                columns: new[] { "OutputId", "FactoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromFactories",
                table: "WarehouseInputFromFactories",
                columns: new[] { "WarehouseInputId", "FactoryId" });

            migrationBuilder.CreateTable(
                name: "WarehouseInputFromWarehouses",
                columns: table => new
                {
                    WarehouseInputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    WarehousePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WarehouseEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WarehouseAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseInputFromWarehouses", x => new { x.WarehouseInputId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromWarehouses_WarehouseInputs_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "WarehouseInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseInputFromWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputToSuppliers",
                columns: table => new
                {
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SupplierPhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SupplierEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SupplierAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputToSuppliers", x => new { x.OutputId, x.SourceId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToSuppliers_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToSuppliers_WarehouseSuppliers_SourceId",
                        column: x => x.SourceId,
                        principalTable: "WarehouseSuppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "WarehouseOutputToWarehouses",
                columns: table => new
                {
                    OutputId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    WarehousePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    WarehouseEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WarehouseAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseOutputToWarehouses", x => new { x.OutputId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToWarehouses_WarehouseOutputs_OutputId",
                        column: x => x.OutputId,
                        principalTable: "WarehouseOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WarehouseOutputToWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromWarehouses_WarehouseId",
                table: "WarehouseInputFromWarehouses",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToSuppliers_OutputId",
                table: "WarehouseOutputToSuppliers",
                column: "OutputId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToSuppliers_SourceId",
                table: "WarehouseOutputToSuppliers",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToWarehouses_OutputId",
                table: "WarehouseOutputToWarehouses",
                column: "OutputId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToWarehouses_WarehouseId",
                table: "WarehouseOutputToWarehouses",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseFactorys_FactoryId",
                table: "WarehouseInputFromFactories",
                column: "FactoryId",
                principalTable: "WarehouseFactorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromFactories",
                column: "WarehouseId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputToFactories_WarehouseFactorys_FactoryId",
                table: "WarehouseOutputToFactories",
                column: "FactoryId",
                principalTable: "WarehouseFactorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputToFactories_WarehouseOutputs_OutputId",
                table: "WarehouseOutputToFactories",
                column: "OutputId",
                principalTable: "WarehouseOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseFactorys_FactoryId",
                table: "WarehouseInputFromFactories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromFactories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputToFactories_WarehouseFactorys_FactoryId",
                table: "WarehouseOutputToFactories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputToFactories_WarehouseOutputs_OutputId",
                table: "WarehouseOutputToFactories");

            migrationBuilder.DropTable(
                name: "WarehouseInputFromWarehouses");

            migrationBuilder.DropTable(
                name: "WarehouseOutputToSuppliers");

            migrationBuilder.DropTable(
                name: "WarehouseOutputToWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseOutputToFactories",
                table: "WarehouseOutputToFactories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromFactories",
                table: "WarehouseInputFromFactories");

            migrationBuilder.RenameTable(
                name: "WarehouseOutputToFactories",
                newName: "WarehouseOutputToFactorys");

            migrationBuilder.RenameTable(
                name: "WarehouseInputFromFactories",
                newName: "WarehouseInputFromFactorys");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseOutputToFactories_OutputId",
                table: "WarehouseOutputToFactorys",
                newName: "IX_WarehouseOutputToFactorys_OutputId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseOutputToFactories_FactoryId",
                table: "WarehouseOutputToFactorys",
                newName: "IX_WarehouseOutputToFactorys_FactoryId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseInputFromFactories_WarehouseId",
                table: "WarehouseInputFromFactorys",
                newName: "IX_WarehouseInputFromFactorys_WarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseInputFromFactories_FactoryId",
                table: "WarehouseInputFromFactorys",
                newName: "IX_WarehouseInputFromFactorys_FactoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseOutputToFactorys",
                table: "WarehouseOutputToFactorys",
                columns: new[] { "OutputId", "FactoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromFactorys",
                table: "WarehouseInputFromFactorys",
                columns: new[] { "WarehouseInputId", "FactoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromFactorys_WarehouseFactorys_FactoryId",
                table: "WarehouseInputFromFactorys",
                column: "FactoryId",
                principalTable: "WarehouseFactorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromFactorys_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromFactorys",
                column: "WarehouseId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputToFactorys_WarehouseFactorys_FactoryId",
                table: "WarehouseOutputToFactorys",
                column: "FactoryId",
                principalTable: "WarehouseFactorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputToFactorys_WarehouseOutputs_OutputId",
                table: "WarehouseOutputToFactorys",
                column: "OutputId",
                principalTable: "WarehouseOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
