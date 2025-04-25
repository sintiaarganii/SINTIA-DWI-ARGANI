using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalPriceAndStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 11, 27, 29, 780, DateTimeKind.Utc).AddTicks(8404), "0zG4I1DMY6ZzwHlfHjtRhO+NL0esxbz/J6mo45YBcbElgj6nM+4wrfsNL2rkZxcqhaxxK3SYg0jl+QyrMLIx7Q==", "+UnYftTpOFfX1xeJItSCOQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 10, 54, 0, 807, DateTimeKind.Utc).AddTicks(1539), "Qmo7aA91EAzFNCjmy/Z1gqepmxriJl0M0ur174P2RB6dDwaC051RQ5lJrWVBM+W2PgN+UFsyHeyErOG6sPr49w==", "DglN0qg6jyFDZAA4JfeMFQ==" });
        }
    }
}
