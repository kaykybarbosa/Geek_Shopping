using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Model.Context
{
    public class MySqlContextEmail : DbContext
    {
        public MySqlContextEmail() {}
        public MySqlContextEmail(DbContextOptions<MySqlContextEmail> options) : base(options) { }
    
        public DbSet<EmailLog> Emails { get; set; }
    }
}