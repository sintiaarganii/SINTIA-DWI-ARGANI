using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SINTIA_DWI_ARGANI.Migrations
{
    /// <inheritdoc />
    public partial class Deleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ordering");

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 15, 47, 4, 74, DateTimeKind.Utc).AddTicks(5879), "uOSyfvyw+rR5cvFOPDrlIhznu8Z10ktI7F+LOy4/4oUNF65rPqT9PwbMUe9o5ATeeyDFlOH4btHft+bJp3UXtw==", "mw3bmjkxoYXhzNKwwtTS3A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CashierId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    StatusOrders = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordering", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordering_Auth_CashierId",
                        column: x => x.CashierId,
                        principalTable: "Auth",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ordering_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Auth",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 23, 14, 22, 19, 629, DateTimeKind.Utc).AddTicks(8981), "GGzzDyE3qzkRDCi4Frgs4/nyH2uGzJHKbI3ly0F4u0K5Jc14seB516UwY/PYbzLA4h4sSf+BnIFXIEFRrE53SA==", "fG/JI9VW0sbklAlvjAZxTg==" });

            migrationBuilder.CreateIndex(
                name: "IX_Ordering_CashierId",
                table: "Ordering",
                column: "CashierId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordering_ProductId",
                table: "Ordering",
                column: "ProductId");
        }
    }
}
