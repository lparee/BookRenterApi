using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Book",
                table: "BookInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_UserProfile",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "UserProfileUserId",
                table: "Cart",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserProfileUserId",
                table: "Cart",
                column: "UserProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInventory_Cart_CartId",
                table: "BookInventory",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_UserProfile_UserProfileUserId",
                table: "Cart",
                column: "UserProfileUserId",
                principalTable: "UserProfile",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInventory_Cart_CartId",
                table: "BookInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_UserProfile_UserProfileUserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserProfileUserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UserProfileUserId",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Book",
                table: "BookInventory",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_UserProfile",
                table: "Cart",
                column: "UserId",
                principalTable: "UserProfile",
                principalColumn: "UserId");
        }
    }
}
