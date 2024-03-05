using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBupdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookInventory_Cart_CartId",
                table: "BookInventory");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Book",
                table: "BookInventory",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Book",
                table: "BookInventory");

            migrationBuilder.AddForeignKey(
                name: "FK_BookInventory_Cart_CartId",
                table: "BookInventory",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
