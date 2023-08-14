using GeekShopping.CartApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CartApi.Model
{
    [Table("CART_DETAIL")]
    public class CartDetail : BaseEntity
    {

        [Column("CART_HEADER_ID")]
        public long CartHeaderId { get; set; }

        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }

        [Column("PRODUCT_ID")]
        public long ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [Column("COUNT")]
        public int Count { get; set; }
    }
}