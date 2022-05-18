using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace EShop.Shared.Hosting.AspNetCore.Helpers;

public static class SwaggerHelper
{
    public static void Configure(IServiceCollection services, string apiTitle, string apiVersion = "v1", string apiName = "v1")
    {
        services.AddSwaggerGen(options =>
                               {
                                   options.SwaggerDoc(apiName, new OpenApiInfo { Title = apiTitle, Version = apiVersion });
                                   options.DocInclusionPredicate((_, _) => true);
                                   options.CustomSchemaIds(type => type.FullName);
                               });
    }

    public static void ConfigureWithAuth(IServiceCollection services, string authority, Dictionary<string, string> scopes, string apiTitle,
                                         string apiVersion = "v1", string apiName = "v1")
    {
        services.AddAbpSwaggerGenWithOAuth(authority,
                                           scopes,
                                           options =>
                                           {
                                               options.SwaggerDoc(apiName, new OpenApiInfo { Title = apiTitle, Version = apiVersion });
                                               options.DocInclusionPredicate((_, _) => true);
                                               options.CustomSchemaIds(type => type.FullName);
                                           });
    }
}
