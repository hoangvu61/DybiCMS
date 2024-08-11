using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82220 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromFactories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromOrders_WarehouseInputs_WarehouseInputId",
                table: "WarehouseInputFromOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromSuppliers_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromWarehouses_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromWarehouses",
                table: "WarehouseInputFromWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputFromWarehouses_WarehouseId",
                table: "WarehouseInputFromWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromSuppliers",
                table: "WarehouseInputFromSuppliers");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputFromSuppliers_WarehouseId",
                table: "WarehouseInputFromSuppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromOrders",
                table: "WarehouseInputFromOrders");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputFromOrders_WarehouseInputId",
                table: "WarehouseInputFromOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromFactories",
                table: "WarehouseInputFromFactories");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputFromFactories_WarehouseId",
                table: "WarehouseInputFromFactories");

            migrationBuilder.DropColumn(
                name: "WarehouseInputId",
                table: "WarehouseInputFromSuppliers");

            migrationBuilder.DropColumn(
                name: "WarehouseInputId",
                table: "WarehouseInputFromFactories");

            migrationBuilder.RenameColumn(
                name: "WarehouseInputId",
                table: "WarehouseInputFromWarehouses",
                newName: "InputId");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "WarehouseInputFromSuppliers",
                newName: "InputId");

            migrationBuilder.RenameColumn(
                name: "WarehouseInputId",
                table: "WarehouseInputFromOrders",
                newName: "InputId");

            migrationBuilder.RenameColumn(
                name: "WarehouseId",
                table: "WarehouseInputFromFactories",
                newName: "InputId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromWarehouses",
                table: "WarehouseInputFromWarehouses",
                column: "InputId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromSuppliers",
                table: "WarehouseInputFromSuppliers",
                column: "InputId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromOrders",
                table: "WarehouseInputFromOrders",
                column: "InputId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromFactories",
                table: "WarehouseInputFromFactories",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromWarehouses_WarehouseId",
                table: "WarehouseInputFromWarehouses",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseInputs_InputId",
                table: "WarehouseInputFromFactories",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromOrders_WarehouseInputs_InputId",
                table: "WarehouseInputFromOrders",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromSuppliers_WarehouseInputs_InputId",
                table: "WarehouseInputFromSuppliers",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromWarehouses_WarehouseInputs_InputId",
                table: "WarehouseInputFromWarehouses",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseInputs_InputId",
                table: "WarehouseInputFromFactories");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromOrders_WarehouseInputs_InputId",
                table: "WarehouseInputFromOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromSuppliers_WarehouseInputs_InputId",
                table: "WarehouseInputFromSuppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputFromWarehouses_WarehouseInputs_InputId",
                table: "WarehouseInputFromWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromWarehouses",
                table: "WarehouseInputFromWarehouses");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputFromWarehouses_WarehouseId",
                table: "WarehouseInputFromWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromSuppliers",
                table: "WarehouseInputFromSuppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromOrders",
                table: "WarehouseInputFromOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputFromFactories",
                table: "WarehouseInputFromFactories");

            migrationBuilder.RenameColumn(
                name: "InputId",
                table: "WarehouseInputFromWarehouses",
                newName: "WarehouseInputId");

            migrationBuilder.RenameColumn(
                name: "InputId",
                table: "WarehouseInputFromSuppliers",
                newName: "WarehouseId");

            migrationBuilder.RenameColumn(
                name: "InputId",
                table: "WarehouseInputFromOrders",
                newName: "WarehouseInputId");

            migrationBuilder.RenameColumn(
                name: "InputId",
                table: "WarehouseInputFromFactories",
                newName: "WarehouseId");

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseInputId",
                table: "WarehouseInputFromSuppliers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseInputId",
                table: "WarehouseInputFromFactories",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromWarehouses",
                table: "WarehouseInputFromWarehouses",
                columns: new[] { "WarehouseInputId", "WarehouseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromSuppliers",
                table: "WarehouseInputFromSuppliers",
                columns: new[] { "WarehouseInputId", "SourceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromOrders",
                table: "WarehouseInputFromOrders",
                columns: new[] { "WarehouseInputId", "OrderId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputFromFactories",
                table: "WarehouseInputFromFactories",
                columns: new[] { "WarehouseInputId", "FactoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromWarehouses_WarehouseId",
                table: "WarehouseInputFromWarehouses",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromSuppliers_WarehouseId",
                table: "WarehouseInputFromSuppliers",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromOrders_WarehouseInputId",
                table: "WarehouseInputFromOrders",
                column: "WarehouseInputId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromFactories_WarehouseId",
                table: "WarehouseInputFromFactories",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromFactories_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromFactories",
                column: "WarehouseId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromOrders_WarehouseInputs_WarehouseInputId",
                table: "WarehouseInputFromOrders",
                column: "WarehouseInputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromSuppliers_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromSuppliers",
                column: "WarehouseId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputFromWarehouses_WarehouseInputs_WarehouseId",
                table: "WarehouseInputFromWarehouses",
                column: "WarehouseId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
