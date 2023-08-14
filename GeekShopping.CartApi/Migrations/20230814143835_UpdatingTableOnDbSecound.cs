using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CartApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTableOnDbSecound : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER)ID",
                table: "CART_DETAIL");

            migrationBuilder.RenameColumn(
                name: "CART_HEADER)ID",
                table: "CART_DETAIL",
                newName: "CART_HEADER_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CART_DETAIL_CART_HEADER)ID",
                table: "CART_DETAIL",
                newName: "IX_CART_DETAIL_CART_HEADER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER_ID",
                table: "CART_DETAIL",
                column: "CART_HEADER_ID",
                principalTable: "CART_HEADER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER_ID",
                table: "CART_DETAIL");

            migrationBuilder.RenameColumn(
                name: "CART_HEADER_ID",
                table: "CART_DETAIL",
                newName: "CART_HEADER)ID");

            migrationBuilder.RenameIndex(
                name: "IX_CART_DETAIL_CART_HEADER_ID",
                table: "CART_DETAIL",
                newName: "IX_CART_DETAIL_CART_HEADER)ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER)ID",
                table: "CART_DETAIL",
                column: "CART_HEADER)ID",
                principalTable: "CART_HEADER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
