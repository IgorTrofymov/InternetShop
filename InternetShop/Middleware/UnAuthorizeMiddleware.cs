using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace InternetShop.WEB.Middleware
{
    public class UnAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;

        public UnAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);
            if (context.Response.StatusCode == 401)
            {
                context.Response.Redirect("/Account/Login");
            }
        }
    }
}
