using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderDetailRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 10, 54, 0, 807, DateTimeKind.Utc).AddTicks(1539), "Qmo7aA91EAzFNCjmy/Z1gqepmxriJl0M0ur174P2RB6dDwaC051RQ5lJrWVBM+W2PgN+UFsyHeyErOG6sPr49w==", "DglN0qg6jyFDZAA4JfeMFQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 10, 41, 52, 149, DateTimeKind.Utc).AddTicks(9311), "I4qlWpn6mjlKglccX0Nfr+WstgSJgPJhOYchsGEVxZG7Jp12hY3k6UUam6mNLfkkK/OPyrpK/Axc53RFn2XWkA==", "rJpTLywfLA3srDnIx2vCWQ==" });
        }
    }
}
