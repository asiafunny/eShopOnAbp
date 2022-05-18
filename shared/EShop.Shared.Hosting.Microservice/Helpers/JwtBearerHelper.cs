using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Shared.Hosting.Microservice.Helpers;

public static class JwtBearerHelper
{
    public static void Configure(IServiceCollection services, string authority, bool requireHttpsMetadata, string audience)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                              {
                                  options.Authority = authority;
                                  options.RequireHttpsMetadata = requireHttpsMetadata;
                                  options.Audience = audience;
                              });
    }
}
