using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbresetup7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Map",
                table: "CartBookMapping");

            migrationBuilder.CreateIndex(
                name: "IX_CartBookMapping_BookId",
                table: "CartBookMapping",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Map",
                table: "CartBookMapping",
                column: "BookId",
                principalTable: "BookInventory",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Map",
                table: "CartBookMapping");

            migrationBuilder.DropIndex(
                name: "IX_CartBookMapping_BookId",
                table: "CartBookMapping");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Map",
                table: "CartBookMapping",
                column: "CartId",
                principalTable: "BookInventory",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
