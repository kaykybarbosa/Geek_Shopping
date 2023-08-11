using GeekShopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{
    [Table("CART_DETAIL")]
    public class CartDetail : BaseEntity
    {
        public long CartHeaderId { get; set; }

        [ForeignKey("CART_HEADER_ID")]
        public CartHeader CartHeader { get; set; }

        public long ProductId { get; set; }

        [ForeignKey("PRODUCT_ID")]
        public Product Product { get; set; }

        [Column("COUNT")]
        public int Count { get; set; }
    }
}