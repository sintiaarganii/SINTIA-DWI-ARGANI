using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class addedDTOStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 27, 17, 29, 49, 302, DateTimeKind.Utc).AddTicks(7966), "+o13+8MdDpAelVxceHL8GTfO1zVuRBdy7UsfqsH/3PEYGB90jm+lF7PwRPOSaMfSjX+uwO78fmvIIq0HprlCjw==", "K+L4cobAxQlrIF28cEArng==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 27, 0, 23, 9, 878, DateTimeKind.Utc).AddTicks(9618), "+hrwftlybeKaR9fsz63Zerw0tEG/fpfmVc89Af3iw7OySKW05grX+RtjTpqvqn1dB+wG89hCy9H91WaD+Oz1bw==", "WuOPkUbnG4++Om2bU7yWgw==" });
        }
    }
}
