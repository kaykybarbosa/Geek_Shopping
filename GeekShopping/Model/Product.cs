using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GeekShopping.Model.Base;
using GeekShopping.ProductApi.Model.Validation;
using Microsoft.IdentityModel.Tokens;

namespace GeekShopping.Model
{
    [Table("PRODUCTS")]
    public sealed class Product : BaseEntity
    {
        [Required]
        [MaxLength(150)]
        [Column("NANE")]
        public string Name { get; private set; }

        [Required]
        [Range(1, 10000)]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; private set; }

        [StringLength(500)]
        [Column("DESCRIPTION")]
        public string Description { get; private set; }

        [StringLength(50)]
        [Column("CATEGORY_NAME")]
        public string CategoryName { get; private set; }

        [StringLength(300)]
        [Column("IMAGE_URL")]
        public string ImageUrl { get; private set; }

        public Product(long id, string name, decimal price, string description, string categoryName, string imageUrl)
        {
            ValidationDomain(name, price, description, categoryName, imageUrl);

            Id = id;
            Name = name;
            Price = price;
            Description = description;
            CategoryName = categoryName;
            ImageUrl = imageUrl;
        }

        private void ValidationDomain(string name, decimal price, string description, string categoryName, string imagemUrl)
        {
            DomainExceptionValidation.When(name.IsNullOrEmpty(), "Invalid name. 'Name' is required.");
            DomainExceptionValidation.When(name.Length < 3, "Invalid name, too short, minimum 3 characters.");
            DomainExceptionValidation.When(name.Length > 150, "Invalid name, too long, maximum 150 characters.");
            DomainExceptionValidation.When(price < 0, "Invalid price value.");
            DomainExceptionValidation.When(description.IsNullOrEmpty(), "Invalid description. 'Description' is required.");
            DomainExceptionValidation.When(description.Length < 5, "Invalid description, too short, minimum 5 characters.");
            DomainExceptionValidation.When(description.Length > 500, "Invalid description, too long, maximum 500 characters.");
            DomainExceptionValidation.When(categoryName.IsNullOrEmpty(), "Invalid category name. Category Name is required.");
            DomainExceptionValidation.When(categoryName.Length < 5, "Invalid category name, too short, mimimum 5 characters.");
            DomainExceptionValidation.When(categoryName.Length > 50, "Invalid category name, too long, maximum 50 characters.");
            DomainExceptionValidation.When(imagemUrl.Length > 300, "Invalid imgemUrl, too long, maximum 300 characters.");
        }
    }
}