using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Yarp.ReverseProxy.Configuration;

namespace EShop.Shared.Hosting.Gateway.Extensions;

public static class SwaggerUIBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerUIWithYarp(this IApplicationBuilder app, ApplicationInitializationContext context)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
                         {
                             var configuration = context.ServiceProvider.GetRequiredService<IConfiguration>();
                             var logger = context.ServiceProvider.GetRequiredService<ILogger<ApplicationInitializationContext>>();
                             var proxyConfigProvider = context.ServiceProvider.GetRequiredService<IProxyConfigProvider>();
                             var proxyConfig = proxyConfigProvider.GetConfig();
                             var routedClusters = proxyConfig.Clusters.SelectMany(c => c.Destinations ?? new Dictionary<string, DestinationConfig>(), (cluster, destination) => new { cluster.ClusterId, Destination = destination.Value });
                             var groupedClusters = routedClusters.GroupBy(q => q.Destination.Address).Select(t => t.First()).Distinct();
                             foreach (var clusterGroup in groupedClusters)
                             {
                                 var routeConfig = proxyConfig.Routes.FirstOrDefault(route => route.ClusterId == clusterGroup.ClusterId);
                                 if (routeConfig == null)
                                 {
                                     logger.LogWarning($"Swagger UI: Couldn't find route configuration for {clusterGroup.ClusterId}...");
                                     continue;
                                 }

                                 options.SwaggerEndpoint($"{clusterGroup.Destination.Address}/swagger/v1/swagger.json", $"{routeConfig.RouteId.Humanize(LetterCasing.Title)} API");
                                 options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                                 options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                             }
                         });
        return app;
    }
}
