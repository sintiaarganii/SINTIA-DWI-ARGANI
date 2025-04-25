using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDTOOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1, 
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 10, 41, 52, 149, DateTimeKind.Utc).AddTicks(9311), "I4qlWpn6mjlKglccX0Nfr+WstgSJgPJhOYchsGEVxZG7Jp12hY3k6UUam6mNLfkkK/OPyrpK/Axc53RFn2XWkA==", "rJpTLywfLA3srDnIx2vCWQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 22, 17, 56, 5, 306, DateTimeKind.Utc).AddTicks(7660), "Ij2nSbapTRk6JU0bO+H/lvF6e+25CyMjeoHbFm4rTOM10JgejLjQHGtgLclwf9GO0iXf/7Vk0Ei6y1KY6lfsYg==", "2QEz1h2XA+GeAtPFn1E/JQ==" });
        }
    }
}
