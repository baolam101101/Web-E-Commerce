using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserInSellerRequestRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerRequest_Users_UserId",
                table: "SellerRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerRequest",
                table: "SellerRequest");

            migrationBuilder.RenameTable(
                name: "SellerRequest",
                newName: "SellerRequests");

            migrationBuilder.RenameIndex(
                name: "IX_SellerRequest_UserId",
                table: "SellerRequests",
                newName: "IX_SellerRequests_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerRequests",
                table: "SellerRequests",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerRequests",
                table: "SellerRequests");

            migrationBuilder.RenameTable(
                name: "SellerRequests",
                newName: "SellerRequest");

            migrationBuilder.RenameIndex(
                name: "IX_SellerRequests_UserId",
                table: "SellerRequest",
                newName: "IX_SellerRequest_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerRequest",
                table: "SellerRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SellerRequest_Users_UserId",
                table: "SellerRequest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
