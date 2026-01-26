using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SellerRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "SellerRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CartItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CartItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4989), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4992), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4993), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4994), null });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4995), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(5069), "$2a$11$b0a4Gf32lt4eshVhKjqNY.HaTZK2S5IWRBWw6hd7ST33eqVMaoXUy", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 328, DateTimeKind.Utc).AddTicks(4925), "$2a$11$eFTJmZhUTPQgm4fOWci.GurME9Om.ovCVDEXR4JcMfRudXhCkPNSi", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "PasswordHash", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 428, DateTimeKind.Utc).AddTicks(5407), "$2a$11$134esIJ/k57cY4WdVM498e3JlElRU/pIXqrlLeybf36eg/1nautOS", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SellerRequests");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "SellerRequests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CartItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$ZPgnNHpYC752EuWswo0sou/EioWdGUjMm6FWpTuAgvxltlo9hUZqy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$uHTN1jEeNa/b3Dgi71vi9u8sYGrZt0giqFhFBGXTiiFCke3FNOyVO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "PasswordHash",
                value: "$2a$11$SKTGxSGZxOWvznH8Vq9DpeqeZmt65Cw67dqLiH6tcvBjk/eI07ST2");
        }
    }
}
