using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GeekShopping.Dtos.Request
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "'Name' is required.")]
        [StringLength(150, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "'Price' is required.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "'Description' is required.")]
        [StringLength(500, MinimumLength = 5)]
        public string Description { get; set; }

        [DisplayName("Category Name")]
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "'Category Name' is required.")]
        public string CategoryName { get; set; }

        [StringLength(300)]
        public string? ImageUrl { get; set; }
    }
}