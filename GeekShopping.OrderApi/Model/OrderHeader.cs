using GeekShopping.OrderApi.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.OrderApi.Model
{
    [Table("ORDER_HEADER")]
    public class OrderHeader : BaseEntity
    {
        [Column("USER_ID")]
        public string UserId { get; set; }

        [Column("COUPON_CODE")]
        public string CouponCode { get; set; }
        
        [Column("PURCHASE_AMOUNT", TypeName = "decimal(18,0)")]
        public decimal PurchaseAmount { get; set; }
        
        [Column("DISCOUNT_AMOUNT", TypeName = "decimal(18,0)")]
        public decimal DiscountAmount { get; set; }
        
        [Column("FIRST_NAME")]
        public string FirstName { get; set; }

        [Column("LAST_NAME")]
        public string LastName { get; set; }
        
        [Column("PURCHASE_DATE")]
        public DateTime DateTime { get; set; } 
        
        [Column("ORDER_TIME")]
        public DateTime ORDER_TIME { get; set; }
        
        [Column("PHONE_NUMBER")]
        public string Phone { get; set; }

        [Column("E_MAIL")]
        public string Email { get; set; }
        
        [Column("CARD_NUMBER")]
        public string CardNumber { get; set; }
        
        [Column("CVV")]
        public string CVV { get; set; }
        
        [Column("EXPIRY_MONTH_YEAR")]
        public string ExpiryMonthYear { get; set; }
        
        [Column("TOTAL_ITENS")]
        public int ORderTotalItens { get; set; }

        public IEnumerable<OrderDetail> CartDetails { get; set; }

        [Column("PAYMENT_STATUS")]
        public bool PaymentStatus { get; set; }
    }
}