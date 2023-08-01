namespace GeekShopping.Dtos.Request
{
    public class ProductRequest
    {
        public String Name { get; set; }
        public decimal Price { get; set; }
        public String Description { get; set; }
        public String CategoryName { get; set; }
        public String ImageUrl { get; set; }
    }
}
