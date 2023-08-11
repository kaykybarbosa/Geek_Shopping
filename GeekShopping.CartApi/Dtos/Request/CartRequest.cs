using GeekShopping.CartApi.Model;

namespace GeekShopping.CartApi.Dtos.Request
{
    public class CartRequest
    {
        public CartHeaderRequest CartHeader { get; set; }
        public IEnumerable<CartDetailRequest> CartDetails { get; set; }
    }
}