using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbresetup4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UId",
                table: "Cart",
                newName: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cart",
                newName: "UId");
        }
    }
}
