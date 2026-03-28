using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddStockAndSold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sold",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 19, 13, 4, 839, DateTimeKind.Utc).AddTicks(7046));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 19, 13, 4, 839, DateTimeKind.Utc).AddTicks(7049));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 27, 19, 13, 4, 839, DateTimeKind.Utc).AddTicks(7050));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 27, 19, 13, 4, 839, DateTimeKind.Utc).AddTicks(7184), "$2a$11$9BJ6R3jTB2UfBB6ofnu7dOWPAk/N6sRA99yNUpRkUsSwHH4f3SHjO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 27, 19, 13, 4, 939, DateTimeKind.Utc).AddTicks(5155), "$2a$11$afv.798mm7Uw4v1YWAzkL.M9LHpNQUYOIS.N9QnkldPFP0A4ZPUPm" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sold",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 18, 35, 22, 542, DateTimeKind.Utc).AddTicks(9928));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 18, 35, 22, 542, DateTimeKind.Utc).AddTicks(9930));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "CreatedAt",
                value: new DateTime(2026, 3, 26, 18, 35, 22, 542, DateTimeKind.Utc).AddTicks(9930));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 26, 18, 35, 22, 543, DateTimeKind.Utc).AddTicks(19), "$2a$11$G8D2r8qrC/N/o2KHPVH4a.8kQ2uy8A7z9N/mFMQvV3WwMUYiJe6hC" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 26, 18, 35, 22, 642, DateTimeKind.Utc).AddTicks(7218), "$2a$11$DMSqOZvBVVZhNapXbc4bg.l7gnpcFW4tRahc/3VoHznUR1SjEKUDq" });
        }
    }
}
