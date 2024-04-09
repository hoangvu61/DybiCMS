using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityRole");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2f3685b3-c4b3-416b-8420-294486fcefa3"), "649c1521-239f-4b34-80ac-21cebe28a6fb", "Admin", "Admin", "ADMIN" },
                    { new Guid("b273b0ab-e7bb-4e41-b625-e3b169ce074b"), "02734344-5457-42c5-9e0c-48e1c910386e", "User", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2f3685b3-c4b3-416b-8420-294486fcefa3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b273b0ab-e7bb-4e41-b625-e3b169ce074b"));

            migrationBuilder.CreateTable(
                name: "IdentityRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityRole", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "03efc154-8a54-4238-9608-f519bdcc180a", "f6fc2948-0773-4a8a-87f4-6f3f02597b8d", "Admin", "ADMIN" },
                    { "c64c1d44-10df-4d45-84f9-8ec10cf50f52", "fb3b8f90-5a72-4709-9c06-757d69f6f1ec", "User", "USER" }
                });
        }
    }
}
