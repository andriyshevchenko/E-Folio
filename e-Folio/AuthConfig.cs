using IdentityModel;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace eFolio.API
{
    public class AuthConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("e-FolioAPI")
                {
                    ApiSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    UserClaims =
                    {
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Role
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    AccessTokenLifetime = 3600,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"e-FolioAPI"}
                }
            };
        }
    }
}
