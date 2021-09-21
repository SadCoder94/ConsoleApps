using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.Text;

namespace DefaultMiddleWare
{
    public static class DemoMidExtensions
    {
        public static IApplicationBuilder UseDemoMid<T>(this IApplicationBuilder builder, string path, Binding binding)
        {
            var encoder = binding.CreateBindingElements().Find<MessageEncodingBindingElement>()?.CreateMessageEncoderFactory().Encoder;
            return builder.UseMiddleware<DemoMiddleware>(typeof(T), path,encoder);
        }

        public static IApplicationBuilder UseDemoMid(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DemoMiddleware>();
        }
    }
}
