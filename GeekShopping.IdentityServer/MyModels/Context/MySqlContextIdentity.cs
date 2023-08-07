using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.IdentityServer.MyModels.Context
{
    public class MySqlContextIdentity : IdentityDbContext<ApplicationUser>
    {
        public MySqlContextIdentity(DbContextOptions<MySqlContextIdentity> options) : base(options) { }
    }
}
