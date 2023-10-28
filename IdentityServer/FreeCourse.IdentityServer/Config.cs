﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog"){Scopes={"catalog_fullpermission"}},
            new ApiResource("resource_photo_stock"){Scopes={"photo_stock_fullpermission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };



        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
               
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
               new ApiScope("catalog_fullpermission","Catalog Api için Full Erişim"),
               new ApiScope("photo_stock_fullpermission","Photo Stock Api için Full Erişim"),
               new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client
               {
                   ClientName="Asp.Net Core MVC",
                   ClientId="WebMvcClient",
                   ClientSecrets={new Secret("secret".Sha256())},
                   AllowedGrantTypes=GrantTypes.ClientCredentials,
                   AllowedScopes={ "catalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
               }
            };
    }
}