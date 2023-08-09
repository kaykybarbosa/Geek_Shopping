using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";

        public static IEnumerable<IdentityResource> IdetityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        //Api scope: thew client can access
        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope> 
        { 
            new ApiScope("geek_shopping", "GeekShopping Server"),
            new ApiScope(name: "read", "Read Data."),
            new ApiScope(name: "write", "Write Data."),
            new ApiScope(name: "delete", "Delete Data.")
        };

        //Client can solicitation in Application: Generic
        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret(Settings.SecretKey.Sha256())},
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"read", "write", "profile"}
            },
            
            new Client
            {
                ClientId = "geek_shopping",
                ClientSecrets = { new Secret(Settings.SecretKey.Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:4430/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:4430/signout-callback-iodc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.Profile,
                    "geek_shopping"
                }
            }
        };
    }
}