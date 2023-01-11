using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityServer4;
using System.Security.Claims;

namespace SocialNetwork.IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "SocialNetwork",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("SocialNetwork.Secret".Sha256())
                    },
                    AllowedScopes = { "SocialNetworkAPI" }
                },
                new Client
                {
                    ClientId = "SocialNetwork.Portal",
                    ClientName = "SocialNetwork Portal Client",
                    AllowedGrantTypes= GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        configuration["SocialNetwork.Portal:RedirectUris"]
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        configuration["SocialNetwork.Portal:PostLogoutRedirectUris"]
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes= new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Email,
                        "SocialNetworkAPI",
                        "roles"
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> ApiScopes =>
           new ApiScope[]
           {
               new ApiScope("SocialNetworkAPI", "Social Network API")
           };

        public static IEnumerable<ApiResource> ApiResources =>
          new ApiResource[0];

        public static IEnumerable<IdentityResource> IdentityResources =>
          new IdentityResource[]
          {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource("roles", "Your role(s)", new List<string>() { "role" })
          };
    }
}
