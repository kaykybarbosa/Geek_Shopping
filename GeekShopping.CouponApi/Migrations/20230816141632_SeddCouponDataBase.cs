using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GeekShopping.CouponApi.Migrations
{
    /// <inheritdoc />
    public partial class SeddCouponDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DISCOUNT_AMOUNT",
                table: "COUPONS",
                newName: "DISCOUNT_AMOUNT");

            migrationBuilder.InsertData(
                table: "COUPONS",
                columns: new[] { "ID", "COUPON_CODE", "DISCOUNT_AMOUNT" },
                values: new object[,]
                {
                    { 1L, "KBULOSO_23_20", 20m },
                    { 2L, "KBULOSO_23_30", 30m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "COUPONS",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "COUPONS",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.RenameColumn(
                name: "DISCOUNT_AMOUNT",
                table: "COUPONS",
                newName: "DiscountAmount");
        }
    }
}
