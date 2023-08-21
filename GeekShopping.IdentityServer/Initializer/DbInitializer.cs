using GeekShopping.IdentityServer.Configuration;
using GeekShopping.IdentityServer.MyModels;
using GeekShopping.IdentityServer.MyModels.Context;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySqlContextIdentity _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _role;

        public DbInitializer(MySqlContextIdentity context,
            UserManager<ApplicationUser> user,
            RoleManager<IdentityRole> role)
        {
            _context = context;
            _user = user;
            _role = role;
        }

        public void Initializer()
        {
            if (_role.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();
            _role.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "kbuloso-admin",
                Email = "kbulosoadmin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (98) 12345-6789",
                FirtsName = "Kbuloso",
                LastName = "Admin"
            };

            _user.CreateAsync(admin, "Kbuloso@99").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirtsName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirtsName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            ApplicationUser client = new ApplicationUser()
            {
                UserName = "kbuloso-client",
                Email = "kbulosoclient@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "+55 (98) 12345-6789",
                FirtsName = "Kbuloso",
                LastName = "Client"
            };

            _user.CreateAsync(client, "Kbuloso@88").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirtsName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirtsName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}