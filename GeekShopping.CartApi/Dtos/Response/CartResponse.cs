using GeekShopping.CartApi.Dtos.Request;

namespace GeekShopping.CartApi.Dtos.Response
{
    public class CartResponse
    {
        public long Id { get; set; }
        public CartHeaderResponse CartHeader { get; set; }
        public IEnumerable<CartDetailResponse> CartDetails { get; set; }
    }
}
