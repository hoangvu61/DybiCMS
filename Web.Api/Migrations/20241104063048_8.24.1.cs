using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class _8241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SEO",
                table: "ItemCategories",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SEO",
                table: "ItemCategories");
        }
    }
}
