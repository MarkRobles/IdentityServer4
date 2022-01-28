// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
               new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Address()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
          new List<ApiScope>
          {            
            new ApiScope("api1", "My API"),
            new ApiScope(name: "read",   displayName: "Read your data."),
            new ApiScope(name: "write",  displayName: "Write your data."),
            new ApiScope(name: "delete", displayName: "Delete your data.")

          };

        public static IEnumerable<Client> Clients =>
    new List<Client>
    {
        new Client
        {
            ClientId = "client",

            // no interactive user, use the clientid/secret for authentication
            AllowedGrantTypes = GrantTypes.ClientCredentials,

            // secret for authentication
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            //Los scopes solo son para saber si una app tiene permiso de invocar la funcion de escribir datos por wejemplo, pero no por  eso el usuario logueado a traves de esa app 
            //podra escribir datos , si a nivel usuario no tiene ese permiso.
            // scopes that client has access to
          AllowedScopes = { "api1","openid", "profile", "read", "write", "delete" }
        },
           // interactive ASP.NET Core MVC client
        new Client
        {
            ClientId = "mvc",
            ClientSecrets = { new Secret("secret".Sha256()) },

            AllowedGrantTypes = GrantTypes.Code,

            // where to redirect to after login
            RedirectUris = { "https://localhost:44300/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

    AllowOfflineAccess = true,// enable support for refresh tokens

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                 IdentityServerConstants.StandardScopes.Address
                ,"api1"
            }
        }


    };
    }
}