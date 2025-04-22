using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class OrderDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 21, 23, 39, 48, 240, DateTimeKind.Utc).AddTicks(2971), "B1Zx1LZB/gpNa9x+0FrI/fDtBefv9IZa9o1kvotjimDVvaROPl6543Ve4sWA4MxbfR4JODdfmGxzHBShnEZMhA==", "bpo2o3xy2cC2qatFXFeKRA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 21, 15, 50, 57, 69, DateTimeKind.Utc).AddTicks(4526), "pQlWQ/XquALtGlT+GwtBrqWFGv2t7WsEOFUZ0e5CCCYq1h1LRIHtg3T5iLe18hh+BnUT8nT8HqEgDGdyJaU7fQ==", "iAlFCz5CUOfRarC46KLSnw==" });
        }
    }
}
