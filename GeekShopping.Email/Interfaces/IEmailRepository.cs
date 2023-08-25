using GeekShopping.Email.Dto;
using GeekShopping.Email.Model;
using System.Net.Mail;

namespace GeekShopping.Email.Interfaces
{
    public interface IEmailRepository
    {
        Task LogEmail(PaymentResultUpdateMessage message);
        void SendEmail(EmailLog email);
    }
}