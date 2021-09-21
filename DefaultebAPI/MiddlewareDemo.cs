using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultebAPI
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MiddlewareDemo
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MiddlewareDemo(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger("DemoMid");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Executing mid");
            _logger.LogInformation($"{httpContext.Request.Path}");

            //var param = httpContext.Request.Query.Keys.First(x => { if (int.TryParse(x, out int c)) return true; return false; });
            //if(!string.IsNullOrWhiteSpace(param))
                await _next(httpContext);

            _logger.LogInformation($"{httpContext.Response.ContentType}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareDemoExtensions
    {
        public static IApplicationBuilder UseMiddlewareDemo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareDemo>();
        }
    }
}
