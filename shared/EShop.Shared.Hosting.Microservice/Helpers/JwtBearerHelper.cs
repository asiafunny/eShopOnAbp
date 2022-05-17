using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Shared.Hosting.Microservice.Helpers;

public static class JwtBearerHelper
{
    public static void Configure(IServiceCollection services, IConfiguration configuration, string audience)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                              {
                                  options.Authority = configuration["AuthServer:Authority"];
                                  options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                                  options.Audience = audience;
                              });
    }
}
