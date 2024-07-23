using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8227 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputFromFactorys_WarehouseId",
                table: "WarehouseInputFromFactorys");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "WarehouseOutputs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                table: "WarehouseOutputs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputToFactorys_OutputId",
                table: "WarehouseOutputToFactorys",
                column: "OutputId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputs_WarehouseId",
                table: "WarehouseOutputs",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromFactorys_WarehouseId",
                table: "WarehouseInputFromFactorys",
                column: "WarehouseId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputs_Warehouses_WarehouseId",
                table: "WarehouseOutputs",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputs_Warehouses_WarehouseId",
                table: "WarehouseOutputs");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputToFactorys_OutputId",
                table: "WarehouseOutputToFactorys");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputs_WarehouseId",
                table: "WarehouseOutputs");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputFromFactorys_WarehouseId",
                table: "WarehouseInputFromFactorys");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "WarehouseOutputs");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "WarehouseOutputs");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputFromFactorys_WarehouseId",
                table: "WarehouseInputFromFactorys",
                column: "WarehouseId");
        }
    }
}
