using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace InternetShop.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            string token = string.Empty;
            if (context.Request.Cookies.ContainsKey("Authorization"))
            {
                token = context.Request.Cookies["Authorization"];
            }

            context.Request.Headers.Add("Authorization",new StringValues(token));
            await _next.Invoke(context);
        }
    }
}
