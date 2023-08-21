using GeekShopping.CartApi.Interfaces.IRepositories;
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

        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task<CartHeader> FindCartHeaderNoTracking(string userId)
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

        public async Task<CartDetail> FindCartDetailNoTracking(long cartDetailProductId, long cartHeaderId)
        {
            var result = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(p =>
                p.ProductId == cartDetailProductId && p.CartHeaderId == cartHeaderId);

            return result;
        }

        public async Task<CartHeader> FindCartHeader(string userId)
        {
            return await _context.CartHeaders.FirstOrDefaultAsync(p =>
                p.UserId == userId);
        }

        public async Task CreateCartHeader(CartHeader cartHeader)
        {
            _context.CartHeaders.Add(cartHeader);
            await _context.SaveChangesAsync();
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

        public async Task UpdateHeader(CartHeader cartHeader)
        {
            _context.CartHeaders.Update(cartHeader);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartDetail>> FindCartDetails(long cartHeaderId)
        {
            return _context.CartDetails.Where(c => c.CartHeaderId == cartHeaderId)
                .Include(c => c.Product);
        }

        public async Task<CartDetail> FindCartDetail(long cartDetailId)
        {
            return await _context.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailId);
        }

        public async Task RemoveCartDetail(CartDetail cartDetail)
        {
            _context.CartDetails.Remove(cartDetail);
            await _context.SaveChangesAsync();
        }

        public async Task<CartHeader> FindCartHeader(long cartHeaderId)
        {
            return await _context.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartHeaderId);
        }

        public async Task RemoveCartHeader(CartHeader cartHeader)
        {
            _context.CartHeaders.Remove(cartHeader);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCartDetailRange(long cartHeaderId)
        {
            _context.CartDetails.RemoveRange(_context.CartDetails.Where(c =>
            c.CartHeaderId == cartHeaderId));

            await _context.SaveChangesAsync();
        }
    }
}