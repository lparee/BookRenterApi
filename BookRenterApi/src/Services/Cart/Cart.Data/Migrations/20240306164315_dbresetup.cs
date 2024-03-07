using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbresetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookInventory",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book_BookId", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", maxLength: 128, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "Guest"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile_UserId", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserPrfUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart_CartId", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_Cart_UserProfile_UserPrfUserId",
                        column: x => x.UserPrfUserId,
                        principalTable: "UserProfile",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartBookMapping",
                columns: table => new
                {
                    CBMappingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartBookMapping", x => x.CBMappingId);
                    table.ForeignKey(
                        name: "FK_Book_Map",
                        column: x => x.CartId,
                        principalTable: "BookInventory",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Map",
                        column: x => x.CartId,
                        principalTable: "Cart",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BookInventory",
                columns: new[] { "BookId", "Author", "BookName", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Chetan", "Hamlet", 100m, 3 },
                    { 2, "Bhagat", "Hamlet1", 101m, 4 },
                    { 3, "Amish", "Hamlet2", 102m, 5 },
                    { 4, "Harrish", "Hamlet3", 103m, 2 },
                    { 5, "Martin", "Hamlet4", 104m, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserProfile",
                columns: new[] { "UserId", "DisplayName", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Guest", "abc@bca.com", "Guest", "" },
                    { 2, "Robert", "robert@martin.com", "Robert", "Martin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserPrfUserId",
                table: "Cart",
                column: "UserPrfUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartBookMapping_CartId",
                table: "CartBookMapping",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartBookMapping");

            migrationBuilder.DropTable(
                name: "BookInventory");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "UserProfile");
        }
    }
}
