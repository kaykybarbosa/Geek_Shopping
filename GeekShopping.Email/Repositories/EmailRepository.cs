using GeekShopping.Email.Dto;
using GeekShopping.Email.Interfaces;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySqlContextEmail> _context;
        public EmailRepository(DbContextOptions<MySqlContextEmail> context)
        {
            _context = context;
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
        }
    }
}