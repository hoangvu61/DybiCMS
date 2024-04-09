using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesv12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2f3685b3-c4b3-416b-8420-294486fcefa3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b273b0ab-e7bb-4e41-b625-e3b169ce074b"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("04a74d65-509e-40ee-945c-36f4a1ddc392"), "e480a47b-d340-4ad7-a578-352c68338fb6", "Product", "Product", "PRODUCT" },
                    { new Guid("637e62e6-87bb-4e76-94ab-637dc1179baa"), "b4f6de73-c865-4a6d-8a66-1eab5788fe99", "Admin", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("04a74d65-509e-40ee-945c-36f4a1ddc392"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("637e62e6-87bb-4e76-94ab-637dc1179baa"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2f3685b3-c4b3-416b-8420-294486fcefa3"), "649c1521-239f-4b34-80ac-21cebe28a6fb", "Admin", "Admin", "ADMIN" },
                    { new Guid("b273b0ab-e7bb-4e41-b625-e3b169ce074b"), "02734344-5457-42c5-9e0c-48e1c910386e", "User", "User", "USER" }
                });
        }
    }
}
