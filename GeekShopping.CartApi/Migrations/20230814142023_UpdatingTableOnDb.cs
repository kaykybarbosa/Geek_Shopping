using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CartApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingTableOnDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER_ID",
                table: "CART_DETAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_CART_DETAIL_PRODUCTS_PRODUCT_ID",
                table: "CART_DETAIL");

            migrationBuilder.DropIndex(
                name: "IX_CART_DETAIL_CART_HEADER_ID",
                table: "CART_DETAIL");

            migrationBuilder.DropColumn(
                name: "CART_HEADER_ID",
                table: "CART_DETAIL");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CART_DETAIL");

            migrationBuilder.RenameColumn(
                name: "CartHeaderId",
                table: "CART_DETAIL",
                newName: "CART_HEADER)ID");

            migrationBuilder.AlterColumn<long>(
                name: "PRODUCT_ID",
                table: "CART_DETAIL",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CART_DETAIL_CART_HEADER)ID",
                table: "CART_DETAIL",
                column: "CART_HEADER)ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER)ID",
                table: "CART_DETAIL",
                column: "CART_HEADER)ID",
                principalTable: "CART_HEADER",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CART_DETAIL_PRODUCTS_PRODUCT_ID",
                table: "CART_DETAIL",
                column: "PRODUCT_ID",
                principalTable: "PRODUCTS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER)ID",
                table: "CART_DETAIL");

            migrationBuilder.DropForeignKey(
                name: "FK_CART_DETAIL_PRODUCTS_PRODUCT_ID",
                table: "CART_DETAIL");

            migrationBuilder.DropIndex(
                name: "IX_CART_DETAIL_CART_HEADER)ID",
                table: "CART_DETAIL");

            migrationBuilder.RenameColumn(
                name: "CART_HEADER)ID",
                table: "CART_DETAIL",
                newName: "CartHeaderId");

            migrationBuilder.AlterColumn<long>(
                name: "PRODUCT_ID",
                table: "CART_DETAIL",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CART_HEADER_ID",
                table: "CART_DETAIL",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "CART_DETAIL",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_CART_DETAIL_CART_HEADER_ID",
                table: "CART_DETAIL",
                column: "CART_HEADER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER_ID",
                table: "CART_DETAIL",
                column: "CART_HEADER_ID",
                principalTable: "CART_HEADER",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CART_DETAIL_PRODUCTS_PRODUCT_ID",
                table: "CART_DETAIL",
                column: "PRODUCT_ID",
                principalTable: "PRODUCTS",
                principalColumn: "ID");
        }
    }
}
