using Microsoft.AspNetCore.Identity;

namespace GeekShopping.IdentityServer.MyModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirtsName { get; set; }
        public string LastName { get; set; }
    }
}
