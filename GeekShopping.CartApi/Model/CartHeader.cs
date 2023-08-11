using GeekShopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{
    [Table("CART_HEADER")]
    public class CartHeader : BaseEntity
    {
        [Column("USER_ID")]
        public string UserId { get; set; }

        [Column("COUPON_CODE")]
        public string CouponCode { get; set; }
    }
}