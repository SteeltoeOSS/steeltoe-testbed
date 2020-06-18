using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Steeltoe.Management.Endpoint;
using System;
using System.Collections.Generic;

namespace Steeltoe.Management.Endpoint.Health
{
    public static class EndpointBuilderExtensions
    {
        public static void MapHealth(this IEndpointRouteBuilder endpoints)
        {
            if (endpoints == null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            var healthOptions = endpoints.ServiceProvider.GetService<IHealthOptions>();
            var options = endpoints.ServiceProvider.GetService<IEnumerable<IManagementOptions>>();

            foreach (var mgmtOptions in options)
            {
                var contextPath = mgmtOptions.Path;
                if (!contextPath.EndsWith("/") && !string.IsNullOrEmpty(healthOptions.Path))
                {
                    contextPath += "/";
                }

                var fullPath = contextPath + healthOptions.Path;
                var pipeline = endpoints.CreateApplicationBuilder()
                    .UseMiddleware<HealthEndpointMiddleware>()
                    .Build();

                endpoints.Map(fullPath, pipeline);
            }
        }
    }
}