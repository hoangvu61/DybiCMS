using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82221 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputProductCodes_ProductCode_ProductId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseOutputProductCodes",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductCodes_OutputId_ProductId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputProductCodes",
                table: "WarehouseInputProductCodes");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputProductCodes_InputId_ProductId",
                table: "WarehouseInputProductCodes");

            migrationBuilder.AddColumn<Guid>(
                name: "InputId",
                table: "WarehouseOutputProductCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseOutputProductCodes",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "OutputId", "ProductId", "ProductCode" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputProductCodes",
                table: "WarehouseInputProductCodes",
                columns: new[] { "InputId", "ProductId", "ProductCode" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductCodes_InputId_ProductId_ProductCode",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "InputId", "ProductId", "ProductCode" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputProductCodes_InputId_ProductId_ProductCode",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "InputId", "ProductId", "ProductCode" },
                principalTable: "WarehouseInputProductCodes",
                principalColumns: new[] { "InputId", "ProductId", "ProductCode" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputs_InputId",
                table: "WarehouseOutputProductCodes",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseOutputs_OutputId",
                table: "WarehouseOutputProductCodes",
                column: "OutputId",
                principalTable: "WarehouseOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputProductCodes_InputId_ProductId_ProductCode",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputs_InputId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseOutputs_OutputId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseOutputProductCodes",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductCodes_InputId_ProductId_ProductCode",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseInputProductCodes",
                table: "WarehouseInputProductCodes");

            migrationBuilder.DropColumn(
                name: "InputId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseOutputProductCodes",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "ProductCode", "ProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseInputProductCodes",
                table: "WarehouseInputProductCodes",
                columns: new[] { "ProductCode", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductCodes_OutputId_ProductId",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "OutputId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProductCodes_InputId_ProductId",
                table: "WarehouseInputProductCodes",
                columns: new[] { "InputId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputProductCodes_ProductCode_ProductId",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "ProductCode", "ProductId" },
                principalTable: "WarehouseInputProductCodes",
                principalColumns: new[] { "ProductCode", "ProductId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
