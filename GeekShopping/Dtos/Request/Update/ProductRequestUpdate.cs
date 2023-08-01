namespace GeekShopping.Dtos.Request.Update
{
    public class ProductRequestUpdate
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public decimal Price { get; set; }
        public String Description { get; set; }
        public String CategoryName { get; set; }
        public String ImageUrl { get; set; }
    }
}
