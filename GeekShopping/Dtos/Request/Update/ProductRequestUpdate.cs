using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Dtos.Request.Update
{
    public class ProductRequestUpdate
    {
        [Required]
        public long Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string CategoryName { get; set; }
        [StringLength(300)]
        public string? ImageUrl { get; set; }
    }
}
