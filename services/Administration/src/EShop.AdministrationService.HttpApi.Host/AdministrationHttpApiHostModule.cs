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
[DependsOn(typeof(AdministrationApplicationModule))]
[DependsOn(typeof(AdministrationEntityFrameworkCoreModule))]
[DependsOn(typeof(AdministrationHttpApiModule))]
public class AdministrationHttpApiHostModule : AbpModule
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
                                               options.ConventionalControllers.Create(typeof(AdministrationHttpApiHostModule).Assembly);
                                           });
    }

    private void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                              {
                                  options.Authority = configuration["AuthServer:Authority"];
                                  options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                                  options.Audience = AdministrationRemoteServiceConsts.RemoteServiceName;
                              });
    }

    private void ConfigureSwaggerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAbpSwaggerGenWithOAuth(configuration["AuthServer:Authority"],
                                           new Dictionary<string, string> { { AdministrationRemoteServiceConsts.RemoteServiceName, AdministrationRemoteServiceConsts.RemoteServiceDescription } },
                                           options =>
                                           {
                                               options.SwaggerDoc("v1", new OpenApiInfo { Title = AdministrationRemoteServiceConsts.RemoteServiceDescription, Version = "v1" });
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
                             options.SwaggerEndpoint("/swagger/v1/swagger.json", AdministrationRemoteServiceConsts.RemoteServiceDescription);
                             options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                             options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
                             options.OAuthScopes(AdministrationRemoteServiceConsts.RemoteServiceName);
                         });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints();
    }

    #endregion

}
