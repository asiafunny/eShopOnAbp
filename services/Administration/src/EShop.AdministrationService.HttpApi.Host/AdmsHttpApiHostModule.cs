using System;
using System.Collections.Generic;
using EShop.AdministrationService.EntityFrameworkCore;
using EShop.Shared.Hosting.Microservice;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace EShop.AdministrationService;

[DependsOn(typeof(SharedHostingMicroserviceModule))]
[DependsOn(typeof(AdmsApplicationModule))]
[DependsOn(typeof(AdmsEntityFrameworkCoreModule))]
[DependsOn(typeof(AdmsHttpApiModule))]
public class AdmsHttpApiHostModule : AbpModule
{

    #region Overrides of AbpModule

    /// <inheritdoc />
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var services = context.Services;
        var configuration = context.Services.GetConfiguration();
        ConfigureConventionalControllers();
        ConfigureAuthentication(services, configuration);
        ConfigureSwaggerServices(services, configuration);
    }

    #endregion

    private void ConfigureConventionalControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
                                           {
                                               options.ConventionalControllers.Create(typeof(AdmsApplicationModule).Assembly,
                                                                                      setting =>
                                                                                      {
                                                                                          setting.RootPath = "eshop";
                                                                                      });
                                           });
    }

    private void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                              {
                                  options.Authority = configuration["AuthServer:Authority"];
                                  options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                                  options.Audience = AdmsRemoteServiceConsts.RemoteServiceName;
                              });
    }

    private void ConfigureSwaggerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAbpSwaggerGenWithOAuth(configuration["AuthServer:Authority"],
                                           new Dictionary<string, string> { { AdmsRemoteServiceConsts.RemoteServiceName, AdmsRemoteServiceConsts.RemoteServiceDescription } },
                                           options =>
                                           {
                                               options.SwaggerDoc("v1", new OpenApiInfo { Title = AdmsRemoteServiceConsts.RemoteServiceDescription, Version = "v1" });
                                               options.DocInclusionPredicate((_, _) => true);
                                               options.CustomSchemaIds(type => type.FullName);
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
                             options.SwaggerEndpoint("/swagger/v1/swagger.json", AdmsRemoteServiceConsts.RemoteServiceDescription);
                             options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                             options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                             options.OAuthScopes(AdmsRemoteServiceConsts.RemoteServiceName);
                         });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }

    #endregion

}
