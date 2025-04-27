using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDTO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2025, 4, 27, 0, 23, 9, 878, DateTimeKind.Utc).AddTicks(9618), "+hrwftlybeKaR9fsz63Zerw0tEG/fpfmVc89Af3iw7OySKW05grX+RtjTpqvqn1dB+wG89hCy9H91WaD+Oz1bw==", "WuOPkUbnG4++Om2bU7yWgw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new DateTime(2025, 4, 26, 10, 48, 24, 213, DateTimeKind.Utc).AddTicks(8755), "Swj8oRdeeuvIpVnd3F2FePhlJBtE0Q/DMA4RWkJ3GMDvXtpXgneUrkFnKKIWjF+Q9NIcvvGkNq/YD50lD8zjdg==", "QVzGql3Pu6i/yQeUWu/mMQ==" });

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
    }
}
