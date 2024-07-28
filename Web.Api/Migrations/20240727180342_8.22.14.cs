using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _82214 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Debt",
                table: "DebtSuppliers",
                newName: "Price");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WarehouseSuppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Warehouses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WarehouseFactorys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WarehouseSuppliers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WarehouseFactorys");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "DebtSuppliers",
                newName: "Debt");
        }
    }
}
