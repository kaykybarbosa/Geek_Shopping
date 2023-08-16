using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GeekShopping.CouponApi.Model.Base;

namespace GeekShopping.CouponApi.Model
{
    [Table("COUPONS")]
    public class Coupon : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        [Column("COUPON_CODE")]
        public String CouponCode { get; set; }

        [Required]
        [Column("DISCOUNT_AMOUNT", TypeName = "decimal(18,0)")]
        public decimal DiscountAmount { get; set; }
    }
}