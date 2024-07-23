using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8228 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProductCodes_ItemProducts_ProductId",
                table: "WarehouseInputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProductCodes_WarehouseInputs_InputId",
                table: "WarehouseInputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_ItemProducts_ProductId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseOutputs_OutputId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductDetails_ItemProducts_ProductId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseInputs_InputId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseOutputs_OutputId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductDetails_InputId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductDetails_ProductId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductCodes_OutputId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductCodes_ProductId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputProductCodes_InputId",
                table: "WarehouseInputProductCodes");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputProductCodes_ProductId",
                table: "WarehouseInputProductCodes");

            migrationBuilder.AddColumn<string>(
                name: "WarehouseInputProductCodeProductCode",
                table: "WarehouseInputProducts",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductDetails_InputId_ProductId",
                table: "WarehouseOutputProductDetails",
                columns: new[] { "InputId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductDetails_OutputId_ProductId",
                table: "WarehouseOutputProductDetails",
                columns: new[] { "OutputId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductCodes_OutputId_ProductId",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "OutputId", "ProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProducts_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts",
                columns: new[] { "WarehouseInputProductCodeProductCode", "WarehouseInputProductCodeProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProductCodes_InputId_ProductId",
                table: "WarehouseInputProductCodes",
                columns: new[] { "InputId", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProductCodes_WarehouseInputProducts_InputId_ProductId",
                table: "WarehouseInputProductCodes",
                columns: new[] { "InputId", "ProductId" },
                principalTable: "WarehouseInputProducts",
                principalColumns: new[] { "InputId", "ProductId" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputProductCodes_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts",
                columns: new[] { "WarehouseInputProductCodeProductCode", "WarehouseInputProductCodeProductId" },
                principalTable: "WarehouseInputProductCodes",
                principalColumns: new[] { "ProductCode", "ProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputProductCodes_ProductCode_ProductId",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "ProductCode", "ProductId" },
                principalTable: "WarehouseInputProductCodes",
                principalColumns: new[] { "ProductCode", "ProductId" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseOutputProducts_OutputId_ProductId",
                table: "WarehouseOutputProductCodes",
                columns: new[] { "OutputId", "ProductId" },
                principalTable: "WarehouseOutputProducts",
                principalColumns: new[] { "OutputId", "ProductId" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseInputProducts_InputId_ProductId",
                table: "WarehouseOutputProductDetails",
                columns: new[] { "InputId", "ProductId" },
                principalTable: "WarehouseInputProducts",
                principalColumns: new[] { "InputId", "ProductId" },
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseOutputProducts_OutputId_ProductId",
                table: "WarehouseOutputProductDetails",
                columns: new[] { "OutputId", "ProductId" },
                principalTable: "WarehouseOutputProducts",
                principalColumns: new[] { "OutputId", "ProductId" },
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProductCodes_WarehouseInputProducts_InputId_ProductId",
                table: "WarehouseInputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseInputProducts_WarehouseInputProductCodes_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseInputProductCodes_ProductCode_ProductId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseOutputProducts_OutputId_ProductId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseInputProducts_InputId_ProductId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseOutputProducts_OutputId_ProductId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductDetails_InputId_ProductId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductDetails_OutputId_ProductId",
                table: "WarehouseOutputProductDetails");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseOutputProductCodes_OutputId_ProductId",
                table: "WarehouseOutputProductCodes");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputProducts_WarehouseInputProductCodeProductCode_WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts");

            migrationBuilder.DropIndex(
                name: "IX_WarehouseInputProductCodes_InputId_ProductId",
                table: "WarehouseInputProductCodes");

            migrationBuilder.DropColumn(
                name: "WarehouseInputProductCodeProductCode",
                table: "WarehouseInputProducts");

            migrationBuilder.DropColumn(
                name: "WarehouseInputProductCodeProductId",
                table: "WarehouseInputProducts");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductDetails_InputId",
                table: "WarehouseOutputProductDetails",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductDetails_ProductId",
                table: "WarehouseOutputProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductCodes_OutputId",
                table: "WarehouseOutputProductCodes",
                column: "OutputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseOutputProductCodes_ProductId",
                table: "WarehouseOutputProductCodes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProductCodes_InputId",
                table: "WarehouseInputProductCodes",
                column: "InputId");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseInputProductCodes_ProductId",
                table: "WarehouseInputProductCodes",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProductCodes_ItemProducts_ProductId",
                table: "WarehouseInputProductCodes",
                column: "ProductId",
                principalTable: "ItemProducts",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseInputProductCodes_WarehouseInputs_InputId",
                table: "WarehouseInputProductCodes",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_ItemProducts_ProductId",
                table: "WarehouseOutputProductCodes",
                column: "ProductId",
                principalTable: "ItemProducts",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductCodes_WarehouseOutputs_OutputId",
                table: "WarehouseOutputProductCodes",
                column: "OutputId",
                principalTable: "WarehouseOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductDetails_ItemProducts_ProductId",
                table: "WarehouseOutputProductDetails",
                column: "ProductId",
                principalTable: "ItemProducts",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseInputs_InputId",
                table: "WarehouseOutputProductDetails",
                column: "InputId",
                principalTable: "WarehouseInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseOutputProductDetails_WarehouseOutputs_OutputId",
                table: "WarehouseOutputProductDetails",
                column: "OutputId",
                principalTable: "WarehouseOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
