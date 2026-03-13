using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 14, 7, 52, 113, DateTimeKind.Utc).AddTicks(5712));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 14, 7, 52, 113, DateTimeKind.Utc).AddTicks(5714));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 14, 7, 52, 113, DateTimeKind.Utc).AddTicks(5715));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 14, 7, 52, 113, DateTimeKind.Utc).AddTicks(5805), "$2a$11$uGRnmRFUoLz.BLaNsSzsFu8V3YAxeuFpixrw1euOrjxtEihoULcOO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 14, 7, 52, 213, DateTimeKind.Utc).AddTicks(5753), "$2a$11$jIAHKcjNKLQIEyVuv/cj9unmsqbMQWOSeNUFHWhi4A/U/HpLDVj9." });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 13, 49, 37, 919, DateTimeKind.Utc).AddTicks(1116));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 13, 49, 37, 919, DateTimeKind.Utc).AddTicks(1119));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 13, 49, 37, 919, DateTimeKind.Utc).AddTicks(1120));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 13, 49, 37, 919, DateTimeKind.Utc).AddTicks(1197), "$2a$11$mdnE/5whsNt5e2Kre7zCE.vEizgiM6sHdakHL.eDRkXIwgdCBVo4W" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 3, 13, 13, 49, 38, 20, DateTimeKind.Utc).AddTicks(4607), "$2a$11$166hCTm9eNkjZbBbTowife0HuKY4mJq62sklf6xmKPwn1VZG4Ln6K" });
        }
    }
}
