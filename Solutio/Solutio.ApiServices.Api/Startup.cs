using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Solutio.ApiServices.Api.Builder;
using Solutio.ApiServices.Api.Filters;
using Solutio.ApiServices.Api.Swagger;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.ApplicationServices.LoginServices;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using Solutio.Core.Services.ServicesProviders;
using Solutio.Core.Services.ServicesProviders.ClaimsServices;
using Solutio.Core.Services.ServicesProviders.ClaimsStatesServices;
using Solutio.Core.Services.ServicesProviders.LoginServices;
using Solutio.Infrastructure.Repositories.Claims;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using Swashbuckle.AspNetCore.Swagger;

namespace Solutio.ApiServices.Api
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
            var policy = new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy();

            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", policy);
            });

            //services.AddApiVersioning(o =>
            //{
            //    o.ReportApiVersions = true;
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.DefaultApiVersion = new ApiVersion(1, 0);
            //});

            //db context
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //Identity
            services.AddIdentity<ApplicationUser, IdentityRole<int>>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.User.RequireUniqueEmail = true;
            })
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddDefaultTokenProviders();

            //Configure mail
            services.AddOptions();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

            //Filter for validation
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            services.AddMvc(opt => opt.Filters.Add(typeof(ModelStateFilter)))
            .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            SetupDependenciesInjection(services);
            SetupSwagger(services);
        }

        private void SetupDependenciesInjection(IServiceCollection services)
        {
            #region Repositories Settings

            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<IClaimStateRepository, ClaimStateRepository>();

            #endregion Repositories Settings

            #region Mappers Settings


            #endregion

            #region Services Settings

            services.AddTransient<INewClaimService, NewClaimService>();
            services.AddTransient<IClaimStateService, ClaimStateService>();
            services.AddSingleton<Solutio.Core.Services.ApplicationServices.IEmailSender, EmailSender>();
            services.AddTransient<ISendConfirmationEmailService, SendConfirmationEmailService>();

            #endregion Services Settings

            #region Builder Settings

            services.AddTransient<ITokenBuilder, TokenBuilder>();

            #endregion Builder Settings
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //.UseHttpsRedirection()
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerConfiguration.EndpointUrl, SwaggerConfiguration.EndpointDescription);
            });

            app.UseCors("AllowOrigin");

            app.UseMvc();
        }

        private void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                var contact = new Contact() { Url = SwaggerConfiguration.ContactUrl };
                swagger.SwaggerDoc(SwaggerConfiguration.DocNameV1,
                                   new Info
                                   {
                                       Title = SwaggerConfiguration.DocInfoTitle,
                                       Version = SwaggerConfiguration.DocInfoVersion,
                                       Description = SwaggerConfiguration.DocInfoDescription,
                                       Contact = contact
                                   });
            });
        }
    }
}
