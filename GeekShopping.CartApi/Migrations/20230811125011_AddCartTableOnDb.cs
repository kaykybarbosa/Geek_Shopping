using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.CartApi.Migrations
{
    /// <inheritdoc />
    public partial class AddCartTableOnDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CART_HEADER",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COUPON_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CART_HEADER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false),
                    NANE = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CATEGORY_NAME = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CART_DETAIL",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartHeaderId = table.Column<long>(type: "bigint", nullable: false),
                    CART_HEADER_ID = table.Column<long>(type: "bigint", nullable: true),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    PRODUCT_ID = table.Column<long>(type: "bigint", nullable: true),
                    COUNT = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CART_DETAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CART_DETAIL_CART_HEADER_CART_HEADER_ID",
                        column: x => x.CART_HEADER_ID,
                        principalTable: "CART_HEADER",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_CART_DETAIL_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CART_DETAIL_CART_HEADER_ID",
                table: "CART_DETAIL",
                column: "CART_HEADER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CART_DETAIL_PRODUCT_ID",
                table: "CART_DETAIL",
                column: "PRODUCT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CART_DETAIL");

            migrationBuilder.DropTable(
                name: "CART_HEADER");

            migrationBuilder.DropTable(
                name: "PRODUCTS");
        }
    }
}
