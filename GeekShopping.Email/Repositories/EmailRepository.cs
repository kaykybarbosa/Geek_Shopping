using GeekShopping.Email.Dto;
using GeekShopping.Email.Interfaces;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace GeekShopping.Email.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySqlContextEmail> _context;
        private MailMessage _email;
        public EmailRepository(DbContextOptions<MySqlContextEmail> context)
        {
            _context = context;
            _email = new MailMessage();
        }

        public async Task LogEmail(PaymentResultUpdateMessage message)
        {
            await using var _db = new MySqlContextEmail(_context);

            EmailLog email = new()
            {
                Email = message.Email,
                SentDate = DateTime.Now,
                Log = $"Order - {message.OrderId} has been created successfully!"
            };
            _db.Emails.Add(email);

            await _db.SaveChangesAsync();

            SendEmail(email);
        }

        public void SendEmail(EmailLog email)
        {
            _email.From = new MailAddress("GeekShopping@gmail.com");

            _email.Subject = "GEEK SHOPPING, YOUR PURCHASE!";
            _email.Body = "Congratulations, we have received your request and are preparing for shipment. Thank you very much for purchasing our products.";

            _email.To.Add(email.Email);
            _email.Priority = MailPriority.Normal;

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential("GeekShopping@gmail.com", "passwordGeekShopping"),
                UseDefaultCredentials = false,
            };

            try
            {
                smtp.Send(_email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}