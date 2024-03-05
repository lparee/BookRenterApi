using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBupdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Category",
                table: "BookInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Book",
                table: "Cart");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Cart_BookId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_BookInventory_CategoryId",
                table: "BookInventory");

            migrationBuilder.DropColumn(
                name: "LoginId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BookInventory");

            migrationBuilder.AlterColumn<string>(
                name: "BookName",
                table: "BookInventory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "BookInventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BookInventory_CartId",
                table: "BookInventory",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Book",
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
                name: "FK_Cart_Book",
                table: "BookInventory");

            migrationBuilder.DropIndex(
                name: "IX_BookInventory_CartId",
                table: "BookInventory");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "BookInventory");

            migrationBuilder.AddColumn<string>(
                name: "LoginId",
                table: "UserProfile",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Cart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "BookName",
                table: "BookInventory",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BookInventory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_CategoryId", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_BookId",
                table: "Cart",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInventory_CategoryId",
                table: "BookInventory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Category",
                table: "BookInventory",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Book",
                table: "Cart",
                column: "BookId",
                principalTable: "BookInventory",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
