using GeekShopping.OrderApi.Interfaces;
using GeekShopping.OrderApi.Model;

namespace GeekShopping.OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository repository)
        {
            _orderRepository = repository;
        }

        public async Task<bool> AddOrderHeader(OrderHeader header)
        {
            if(header != null)
            {
                return await _orderRepository.AddOrderHeader(header);
            }
            
            return false;
        }

        public async Task UpdateOrderPaymentStatus(long orderHeaderid, bool status)
        {
            var header = await _orderRepository.GetOrderHeaderById(orderHeaderid);
            if(header != null)
            {
                header.PaymentStatus = status;
                
                await _orderRepository.UpdateOrderPaymentStatus(header);
            }
        }
    }
}