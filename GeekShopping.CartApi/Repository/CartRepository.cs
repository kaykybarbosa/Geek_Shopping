using GeekShopping.CartApi.Interfaces;
using GeekShopping.CartApi.Model;
using GeekShopping.CartApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySqlContextCart _context;

        public CartRepository(MySqlContextCart context)
        {
            _context = context;
        }

        public Task<bool> ApplyCoupon(string userId, string couponCode)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public Task<Cart> FindCartByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CartHeader> FindCartHeader(string userId)
        {
            var cartHeader = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(h =>
                h.UserId == userId);

            return cartHeader;
        }

        public async Task<Product> FindProductById(long id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(product =>
                product.Id == id);

            return product;
        }

        public async Task<CartDetail> FindCartDetail(long cartDetailId, long cartHeaderId)
        {
            var result = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(p =>
                p.Id == cartDetailId && p.CartHeaderId == cartHeaderId);

            return result;
        }

        public Task<bool> RemoveCoupon(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromCart(long cartDetailsId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateCartHeader(CartHeader cartHeader)
        {
            _context.CartHeaders.Add(cartHeader);
            await _context.SaveChangesAsync();
        }

        public async Task<Cart> SaveOrDeleteCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public async Task CreateCartDetails(CartDetail cartDetail)
        {
            _context.CartDetails.Add(cartDetail);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartDetail(CartDetail cartDetails)
        {
           _context.CartDetails.Update(cartDetails);
            await _context.SaveChangesAsync();
        }
    }
}