using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class mappingandseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Book",
                table: "BookInventory");

            migrationBuilder.DropIndex(
                name: "IX_BookInventory_CartId",
                table: "BookInventory");

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
                columns: new[] { "BookId", "Author", "BookName", "CartId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "Chetan", "Hamlet", 0, 100m, 3 },
                    { 2, "Bhagat", "Hamlet1", 0, 101m, 4 },
                    { 3, "Amish", "Hamlet2", 0, 102m, 5 },
                    { 4, "Harrish", "Hamlet3", 0, 103m, 2 },
                    { 5, "Martin", "Hamlet4", 0, 104m, 1 }
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
                name: "IX_CartBookMapping_CartId",
                table: "CartBookMapping",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartBookMapping");

            migrationBuilder.DeleteData(
                table: "BookInventory",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookInventory",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookInventory",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookInventory",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookInventory",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserProfile",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserProfile",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_BookInventory_CartId",
                table: "BookInventory",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Book",
                table: "BookInventory",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
