using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.CouponApi.Model.Base
{
    public class BaseEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
    }
}