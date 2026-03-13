using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Web_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 14, 14, 39, 610, DateTimeKind.Utc).AddTicks(4499));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 14, 14, 39, 610, DateTimeKind.Utc).AddTicks(4500));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 2, 28, 14, 14, 39, 610, DateTimeKind.Utc).AddTicks(4501));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 2, 28, 14, 14, 39, 610, DateTimeKind.Utc).AddTicks(4623), "$2a$11$Y2QwghvQ3WIEebxmXOcgYOkXDdJGLf57uomEu4L72Uot8U8YnBpDm" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UserName" },
                values: new object[] { new DateTime(2026, 2, 28, 14, 14, 39, 783, DateTimeKind.Utc).AddTicks(8937), "$2a$11$aB/wmtofv.sD/0usqfGHg.M1HRu6.diH6YANMOXaXoGyr8F6Wipn6", "seller" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4989));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4992));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4993));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 4, new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4994), "Moderator", null },
                    { 5, new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(4995), "Manager", null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 229, DateTimeKind.Utc).AddTicks(5069), "$2a$11$b0a4Gf32lt4eshVhKjqNY.HaTZK2S5IWRBWw6hd7ST33eqVMaoXUy" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "PasswordHash", "UserName" },
                values: new object[] { new DateTime(2026, 1, 26, 9, 34, 32, 328, DateTimeKind.Utc).AddTicks(4925), "$2a$11$eFTJmZhUTPQgm4fOWci.GurME9Om.ovCVDEXR4JcMfRudXhCkPNSi", "moderator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "FullName", "PasswordHash", "PhoneNumber", "UpdatedAt", "UserName" },
                values: new object[] { 3, null, new DateTime(2026, 1, 26, 9, 34, 32, 428, DateTimeKind.Utc).AddTicks(5407), "", "", "$2a$11$134esIJ/k57cY4WdVM498e3JlElRU/pIXqrlLeybf36eg/1nautOS", "", null, "manager" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 4, 2 },
                    { 5, 3 }
                });
        }
    }
}
