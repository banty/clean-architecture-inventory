using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace CleanArchitectureInventory.Identity.IdentityServerAPI.Configuration;

public static class Config
{

    // ApiResources define the apis in your system
    public static IEnumerable<ApiResource> GetApis()
    {
        return new List<ApiResource>
            {
                new ApiResource("catalogs", "Catalogs Service"),
               
            };
    }

    // ApiScope is used to protect the API 
    //The effect is the same as that of API resources in IdentityServer 3.x
    public static IEnumerable<ApiScope> GetApiScopes =>
    
        new List<ApiScope>
            {
                new ApiScope("catalogs", "Catalogs Service"),
               
            };
    
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

  
    public static IEnumerable<Client> Clients(IConfiguration configuration) =>
        new List<Client>
        {
            
                new Client
                {
                    ClientId = "mvcclient",
                    ClientName = "Inventory Web OpenId Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "https://localhost:5002/signin-oidc" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },
                    
                   // AllowedCorsOrigins =     { $"{configuration["SpaClient"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "catalogs",
                       
                    },
                },
                new Client
                {
                    ClientId = "webclient",
                    ClientName = "Inventory Web OpenId Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = { "https://localhost:5003/signin-oidc" },
                    RequireConsent = false,
                    PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },
                    
                   // AllowedCorsOrigins =     { $"{configuration["SpaClient"]}" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "catalogs",

                    },
                },
                 new Client
                {
                    ClientId = "catalogsswaggerui",
                    ClientName = "Catalog Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["CatalogApiClient"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["CatalogApiClient"]}/swagger/" },

                    AllowedScopes =
                    {
                        "catalogs"
                    }
                },
        };
}

