using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GeekShopping.CartApi.Model.Base;

namespace GeekShopping.CartApi.Model
{
    [Table("PRODUCTS")]
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        [Column("NANE")]
        public String Name { get; set; }

        [Required]
        [Range(1, 10000)]
        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { get; set; }

        [StringLength(500)]
        [Column("DESCRIPTION")]
        public String Description { get; set; }

        [StringLength(50)]
        [Column("CATEGORY_NAME")]
        public String CategoryName { get; set; }

        [StringLength(300)]
        [Column("IMAGE_URL")]
        public String ImageUrl { get; set; }
    }
}