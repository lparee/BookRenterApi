using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbresetup3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_UserProfile_UserPrfUserId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_UserPrfUserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UserPrfUserId",
                table: "Cart");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPrfUserId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserPrfUserId",
                table: "Cart",
                column: "UserPrfUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_UserProfile_UserPrfUserId",
                table: "Cart",
                column: "UserPrfUserId",
                principalTable: "UserProfile",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
