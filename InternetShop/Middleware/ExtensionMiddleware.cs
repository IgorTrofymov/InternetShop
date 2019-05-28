using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetShop.WEB.Middleware;
using Microsoft.AspNetCore.Builder;

namespace InternetShop.Middleware
{
    public static  class ExtensionMiddleware
    {
        public static IApplicationBuilder UseToken(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenMiddleware>();
        }

        public static IApplicationBuilder UseRedirectUnautorize(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UnAuthorizeMiddleware>();
        }
    }
}
