using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class RollbackSellerRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerRequests_Users_SellerId",
                table: "SellerRequests");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "SellerRequests",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerRequests_SellerId",
                table: "SellerRequests",
                newName: "IX_SellerRequests_UserId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "PasswordHash",
                value: "$2a$11$t5B2r3evDxmIEt7lmfV00eFl8Fn1r25NXxISJMOFOt7USxdneQNSC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "PasswordHash",
                value: "$2a$11$n1In2vxHOaBgwiPSwCAylOWpLcomBpBYBhb82Ju4bPBE8kbtDWdyi");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerRequests_Users_UserId",
                table: "SellerRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerRequests_Users_UserId",
                table: "SellerRequests");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "SellerRequests",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerRequests_UserId",
                table: "SellerRequests",
                newName: "IX_SellerRequests_SellerId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "PasswordHash",
                value: "$2a$11$vPt3rNZ.EesqxLOw6P6sm.ut/stcflOWj1gRjR4LWzz4iIkrdsJNq");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "PasswordHash",
                value: "$2a$11$wQXnRidSfKkBjH95lYRnYO1okM7yYi8uhSTTGVKQlQ3rKNy/BhpYe");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerRequests_Users_SellerId",
                table: "SellerRequests",
                column: "SellerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
