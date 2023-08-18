using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeekShopping.OrderApi.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationOnDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ORDER_HEADER",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COUPON_CODE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PURCHASE_AMOUNT = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    DISCOUNT_AMOUNT = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PURCHASE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ORDER_TIME = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    E_MAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CARD_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EXPIRY_MONTH_YEAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TOTAL_ITENS = table.Column<int>(type: "int", nullable: false),
                    PAYMENT_STATUS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_HEADER", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_DETAIL",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDER_HEADER_ID = table.Column<long>(type: "bigint", nullable: false),
                    PRODUCT_ID = table.Column<long>(type: "bigint", nullable: false),
                    COUNT = table.Column<int>(type: "int", nullable: false),
                    PRODUCT_NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRICE = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_DETAIL", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_DETAIL_ORDER_HEADER_ORDER_HEADER_ID",
                        column: x => x.ORDER_HEADER_ID,
                        principalTable: "ORDER_HEADER",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_DETAIL_ORDER_HEADER_ID",
                table: "ORDER_DETAIL",
                column: "ORDER_HEADER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ORDER_DETAIL");

            migrationBuilder.DropTable(
                name: "ORDER_HEADER");
        }
    }
}
