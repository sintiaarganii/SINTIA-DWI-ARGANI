using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class AddReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetails",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 13, 23, 10, 517, DateTimeKind.Utc).AddTicks(8004), "QZZeedfwvfKcsh+qu11V4OOdXQXPbkjap+IHbHcqIdhbv1C4krmnClDfGs1TlINnGhxVJAJATxntVCISwbH3Qw==", "fsfkYCHRq5L4weatKlazlA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 11, 27, 29, 780, DateTimeKind.Utc).AddTicks(8404), "0zG4I1DMY6ZzwHlfHjtRhO+NL0esxbz/J6mo45YBcbElgj6nM+4wrfsNL2rkZxcqhaxxK3SYg0jl+QyrMLIx7Q==", "+UnYftTpOFfX1xeJItSCOQ==" });
        }
    }
}
