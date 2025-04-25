using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class addedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 25, 15, 57, 29, 196, DateTimeKind.Utc).AddTicks(680), "eo4gayu6NEe1xgkeYlINlj2NV3pZXJe6bwq6Fyj7P5ngQ6yz3spIxe71bENtEBBszwlPB/CbQiOgC9wjzv79vw==", "MP9Q/5ssTFmVl+EAFJ0CCQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 16, 40, 36, 325, DateTimeKind.Utc).AddTicks(1721), "Smdb3/jvbAyPmiTVr77oFs3MxAIGYhUdK5IpLRLEa+3WMGIk4HLH4MIPQpzERkUE3WMfpDUkTJ75/7iUhOx52Q==", "eAiwaHZJEw3btoBihxPvEw==" });
        }
    }
}
