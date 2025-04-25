using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class AddValidation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 16, 40, 36, 325, DateTimeKind.Utc).AddTicks(1721), "Smdb3/jvbAyPmiTVr77oFs3MxAIGYhUdK5IpLRLEa+3WMGIk4HLH4MIPQpzERkUE3WMfpDUkTJ75/7iUhOx52Q==", "eAiwaHZJEw3btoBihxPvEw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 15, 47, 4, 74, DateTimeKind.Utc).AddTicks(5879), "uOSyfvyw+rR5cvFOPDrlIhznu8Z10ktI7F+LOy4/4oUNF65rPqT9PwbMUe9o5ATeeyDFlOH4btHft+bJp3UXtw==", "mw3bmjkxoYXhzNKwwtTS3A==" });
        }
    }
}
