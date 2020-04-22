using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using Helpers.ActionFilters;
using Helpers.Services.Contracts;
using Helpers.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WEBAPI.Extensions;
using WEBAPI.Logger;
using WEBAPI.Middlewares;
using WEBAPI.Model;
using WEBAPI.Model.DatabaseModels;
using WEBAPI.Services.Contracts;
using WEBAPI.Services.Implementations;

namespace WEBAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddScoped<ModelValidationAttribute>();

            services.AddDbContext<ApplicationDatabaseContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidateAudience = false,
                        ValidAudience = Configuration["Jwt:Audience"],
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ValidateIssuerSigningKey = true,
                    };
                });

            services.AddAutoMapper();
            services.AddMvc(config =>
            {
                // Add XML Content Negotiation
                config.ReturnHttpNotAcceptable = true;
                config.RespectBrowserAcceptHeader = true;
                config.InputFormatters.Add(new XmlSerializerInputFormatter());
                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            // Injecting services
            services.AddTransient<IHelperService, HelperService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<ICompanyManagerService, CompanyManagerService>(); 
            services.AddTransient<ICompaniesService, CompaniesService>();
            services.AddTransient<ICompanySettingsService, CompanySettingsService>(); 
            services.AddTransient<IUserSettingsService, UserSettingsService>(); 
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<ITableSettingsService, TableSettingsService>();
            services.AddTransient<IActsService, ActsService>();
            services.AddTransient<IBreakService, BreakService>();
            services.AddTransient<ISmsService, SmsService>();


            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Title",
                    Version = $"v1",
                    Description = "Desc",
                    Contact = new OpenApiContact
                    {
                        Name = "Andrey Mensh",
                        Url = new Uri("https://github.com/AndreyMensh")
                    }
                });

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
            });

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDatabaseContext dbcontext)
        {
            dbcontext.Database.Migrate();

            // seed infrastructure
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDatabaseContext>().Started();
            }


            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            app.UseCors(opts => opts.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            app.ConfigureExceptionHandler();

            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                if (context.User.Identity.IsAuthenticated && !context.User.IsInRole("SuperAdmin") && context.Request.Path != "/api/auth")
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var companyId = serviceScope.ServiceProvider.GetService<IHelperService>().GetCompanyId(context.User);
                        var isBlocked = serviceScope.ServiceProvider.GetService<ApplicationDatabaseContext>().Companies.Any(x => x.Id == companyId && x.IsBlocked);

                        if (isBlocked)
                        {
                            serviceScope.ServiceProvider.GetService<IAuthService>().LogOutCompany(companyId);
                            context.Response.StatusCode = 401;
                        }
                        else
                        {
                            await next();
                        }
                    }
                }
                else
                {
                    await next();
                }
            });
            app.UseMvc();



            app.UseSwagger();
            app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBAPI Api");
                    x.OAuthUseBasicAuthenticationWithAccessCodeGrant();
                }
            );
        }
    }
}
