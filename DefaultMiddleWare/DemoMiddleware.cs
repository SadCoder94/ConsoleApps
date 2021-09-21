using Microsoft.AspNetCore.Http;
using System;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace DefaultMiddleWare
{
    public class DemoMiddleware
    {
        private readonly RequestDelegate _next; 
        private readonly Type _serviceType;
        private readonly string _endpointPath;
        private readonly MessageEncoder _messageEncoder;

        public DemoMiddleware(RequestDelegate next, Type serviceType, string path, MessageEncoder encoder)
        {
            _next = next;
            _serviceType = serviceType;
            _endpointPath = path;
            _messageEncoder = encoder;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine($"Request for {context.Request.Path} received");

            await _next.Invoke(context);
        }
    }
}
