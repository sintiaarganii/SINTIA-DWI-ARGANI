using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 14, 22, 19, 629, DateTimeKind.Utc).AddTicks(8981), "GGzzDyE3qzkRDCi4Frgs4/nyH2uGzJHKbI3ly0F4u0K5Jc14seB516UwY/PYbzLA4h4sSf+BnIFXIEFRrE53SA==", "fG/JI9VW0sbklAlvjAZxTg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 13, 23, 10, 517, DateTimeKind.Utc).AddTicks(8004), "QZZeedfwvfKcsh+qu11V4OOdXQXPbkjap+IHbHcqIdhbv1C4krmnClDfGs1TlINnGhxVJAJATxntVCISwbH3Qw==", "fsfkYCHRq5L4weatKlazlA==" });
        }
    }
}
