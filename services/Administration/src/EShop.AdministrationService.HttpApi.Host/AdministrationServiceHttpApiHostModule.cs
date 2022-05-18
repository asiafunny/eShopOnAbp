using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.AdministrationService.EntityFrameworkCore;
using EShop.Shared.Hosting.AspNetCore.Helpers;
using EShop.Shared.Hosting.Microservice;
using EShop.Shared.Hosting.Microservice.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace EShop.AdministrationService;

[DependsOn(typeof(SharedHostingMicroserviceModule))]
[DependsOn(typeof(AdministrationServiceApplicationModule))]
[DependsOn(typeof(AdministrationServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(AdministrationServiceHttpApiModule))]
public class AdministrationServiceHttpApiHostModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        var configuration = context.Services.GetConfiguration();
        JwtBearerHelper.Configure(services, configuration["AuthServer:Authority"], Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]), AdministrationServiceRemoteServiceConsts.RemoteServiceName);
        SwaggerHelper.ConfigureWithAuth(services,
                                        configuration["AuthServer:Authority"],
                                        new Dictionary<string, string> { { AdministrationServiceRemoteServiceConsts.RemoteServiceName, AdministrationServiceRemoteServiceConsts.RemoteServiceDescription } },
                                        AdministrationServiceRemoteServiceConsts.RemoteServiceDescription);
        ConfigureCors(services, configuration);
    }

    #endregion

    private void ConfigureCors(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
                         {
                             options.AddDefaultPolicy(builder =>
                                                      {
                                                          builder.WithOrigins(configuration["App:CorsOrigins"].Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o => o.RemovePostFix("/")).ToArray())
                                                                 .WithAbpExposedHeaders()
                                                                 .SetIsOriginAllowedToAllowWildcardSubdomains()
                                                                 .AllowAnyHeader()
                                                                 .AllowAnyMethod()
                                                                 .AllowCredentials();
                                                      });
                         });
    }

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseCors();
        app.UseAbpRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpClaimsMap();
        // app.UseMultiTenancy();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
                         {
                             var configuration = context.GetConfiguration();
                             options.SwaggerEndpoint("/swagger/v1/swagger.json", AdministrationServiceRemoteServiceConsts.RemoteServiceDescription);
                             options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                             options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                             options.OAuthScopes(AdministrationServiceRemoteServiceConsts.RemoteServiceName);
                         });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }

    /// <inheritdoc />
    public override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        return base.OnPostApplicationInitializationAsync(context);
    }

    #endregion

}
