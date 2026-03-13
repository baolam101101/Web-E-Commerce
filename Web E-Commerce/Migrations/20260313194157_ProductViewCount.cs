using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class ProductViewCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 19, 41, 55, 837, DateTimeKind.Utc).AddTicks(1994));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 19, 41, 55, 837, DateTimeKind.Utc).AddTicks(1998));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 19, 41, 55, 837, DateTimeKind.Utc).AddTicks(1999));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 19, 41, 55, 837, DateTimeKind.Utc).AddTicks(2083), "$2a$11$glca/yco.n7EENrPHOzkxe98I7FCkohy50uVQ9z/pbjbDkAvhvkNu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 19, 41, 55, 938, DateTimeKind.Utc).AddTicks(3905), "$2a$11$776zPKKGU7kaxeIZERkikezSzZWt0FxD8oTVxKnojc/6xiMll3leS" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 18, 58, 45, 639, DateTimeKind.Utc).AddTicks(3030));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 18, 58, 45, 639, DateTimeKind.Utc).AddTicks(3035));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 18, 58, 45, 639, DateTimeKind.Utc).AddTicks(3036));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 18, 58, 45, 639, DateTimeKind.Utc).AddTicks(3125), "$2a$11$5vtvDe8TK4vr.pa5G3edwOOCra.hZspwh3snVHpxMJXCjxNciiuPO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 18, 58, 45, 739, DateTimeKind.Utc).AddTicks(2224), "$2a$11$1RlS0n/W8zep7vof/zi.zuEuZSNUkeA0LkaEYjakLlkd5.dPbLFXm" });
        }
    }
}
