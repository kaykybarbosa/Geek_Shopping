using GeekShopping.OrderApi.Interfaces;
using GeekShopping.OrderApi.Model;
using GeekShopping.OrderApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<MySqlContextOrder> _context;

        public OrderRepository(DbContextOptions<MySqlContextOrder> context)
        {
            _context = context;
        }

        public async Task<bool> AddOrderHeader(OrderHeader header)
        {
            await using var _db = new MySqlContextOrder(_context);

            _db.Headers.Add(header);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<OrderHeader> GetOrderHeaderById(long id)
        {
            await using var _db = new MySqlContextOrder(_context);
            
            return await _db.Headers.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateOrderPaymentStatus(OrderHeader header)
        {
            await using var _db = new MySqlContextOrder(_context);
            
            _db.Headers.Update(header);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderid, bool status)
        {
            await using var _db = new MySqlContextOrder(_context);
            
            var order = await _db.Headers.FirstOrDefaultAsync(o => o.Id == orderHeaderid);
            if(order != null){
                order.PaymentStatus = status;
                
                await _db.SaveChangesAsync();
            }
        }
    }
}