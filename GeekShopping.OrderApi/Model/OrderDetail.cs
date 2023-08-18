using GeekShopping.OrderApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderApi.Model
{
    [Table("ORDER_DETAIL")]
    public class OrderDetail : BaseEntity
    {
        [Column("ORDER_HEADER_ID")]
        public long OrderHeaderId { get; set; }

        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeader CartHeader { get; set; }

        [Column("PRODUCT_ID")]
        public long ProductId { get; set; }

        [Column("COUNT")]
        public int Count { get; set; }
        
        [Column("PRODUCT_NAME")]
        public string ProductName { get; set; }

        [Column("PRICE", TypeName = "decimal(18,0)")]
        public decimal Price { get; set; }
    }
}