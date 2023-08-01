namespace GeekShopping.Dtos.Response
{
    public class AllProductsResponse : BaseResponse
    {
        public IEnumerable<ProductResponse> Products { get; set; }
    }
}
