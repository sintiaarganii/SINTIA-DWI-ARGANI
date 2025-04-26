using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 26, 10, 48, 24, 213, DateTimeKind.Utc).AddTicks(8755), "Swj8oRdeeuvIpVnd3F2FePhlJBtE0Q/DMA4RWkJ3GMDvXtpXgneUrkFnKKIWjF+Q9NIcvvGkNq/YD50lD8zjdg==", "QVzGql3Pu6i/yQeUWu/mMQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 26, 10, 11, 35, 293, DateTimeKind.Utc).AddTicks(6352), "R3AZ3ELBryrc0OWNR8Vc1RxDnhl9p6RVMs8yVR1DInwxcD9vlMBmOCtX1aCpB5/l+iZiUBifqeHPfb6SWBO6sA==", "RcGO7ALwch2gnlCg6tXv3Q==" });
        }
    }
}
