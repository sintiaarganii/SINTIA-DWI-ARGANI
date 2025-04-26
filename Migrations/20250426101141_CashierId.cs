using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class CashierId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CashierId",
                table: "Orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CashierId1",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 26, 10, 11, 35, 293, DateTimeKind.Utc).AddTicks(6352), "R3AZ3ELBryrc0OWNR8Vc1RxDnhl9p6RVMs8yVR1DInwxcD9vlMBmOCtX1aCpB5/l+iZiUBifqeHPfb6SWBO6sA==", "RcGO7ALwch2gnlCg6tXv3Q==" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashierId1",
                table: "Orders",
                column: "CashierId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Auth_CashierId1",
                table: "Orders",
                column: "CashierId1",
                principalTable: "Auth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Auth_CashierId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CashierId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CashierId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CashierId1",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 25, 15, 57, 29, 196, DateTimeKind.Utc).AddTicks(680), "eo4gayu6NEe1xgkeYlINlj2NV3pZXJe6bwq6Fyj7P5ngQ6yz3spIxe71bENtEBBszwlPB/CbQiOgC9wjzv79vw==", "MP9Q/5ssTFmVl+EAFJ0CCQ==" });
        }
    }
}
