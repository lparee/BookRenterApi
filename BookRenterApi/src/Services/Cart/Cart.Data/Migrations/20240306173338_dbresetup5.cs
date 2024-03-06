using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carts.Data.Migrations
{
    /// <inheritdoc />
    public partial class dbresetup5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CartBookMapping",
                table: "CartBookMapping");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapId",
                table: "CartBookMapping",
                column: "CBMappingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MapId",
                table: "CartBookMapping");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartBookMapping",
                table: "CartBookMapping",
                column: "CBMappingId");
        }
    }
}
