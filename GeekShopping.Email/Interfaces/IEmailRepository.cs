using GeekShopping.Email.Dto;

namespace GeekShopping.Email.Interfaces
{
    public interface IEmailRepository
    {
        Task LogEmail(PaymentResultUpdateMessage message);
    }
}