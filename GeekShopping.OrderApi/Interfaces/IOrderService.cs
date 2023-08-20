using GeekShopping.OrderApi.Model;

namespace GeekShopping.OrderApi.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddOrderHeader(OrderHeader header);
        Task UpdateOrderPaymentStatus(long orderHeaderid, bool status);
    }
}