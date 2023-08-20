using GeekShopping.OrderApi.Model;

namespace GeekShopping.OrderApi.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderHeader> GetOrderHeaderById(long id);
        Task<bool> AddOrderHeader(OrderHeader header);
        Task UpdateOrderPaymentStatus(OrderHeader header);
    }
}