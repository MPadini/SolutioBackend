using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using Microsoft.IdentityModel.Tokens;
using Solutio.ApiServices.Api.Builder;
using Solutio.ApiServices.Api.Filters;
using Solutio.ApiServices.Api.Mappers;
using Solutio.ApiServices.Api.Swagger;
using Solutio.Core.Entities;
using Solutio.Core.Services.ApplicationServices.ClaimsServices;
using Solutio.Core.Services.ApplicationServices.ClaimsStatesServices;
using Solutio.Core.Services.ApplicationServices.FileService;
using Solutio.Core.Services.ApplicationServices.Location;
using Solutio.Core.Services.ApplicationServices.LoginServices;
using Solutio.Core.Services.Factories;
using Solutio.Core.Services.Repositories;
using Solutio.Core.Services.Repositories.ClaimsRepositories;
using Solutio.Core.Services.Repositories.Location;
using Solutio.Core.Services.ServicesProviders;
using Solutio.Core.Services.ServicesProviders.ClaimsServices;
using Solutio.Core.Services.ServicesProviders.ClaimsStatesServices;
using Solutio.Core.Services.ServicesProviders.FileService;
using Solutio.Core.Services.ServicesProviders.Location;
using Solutio.Core.Services.ServicesProviders.LoginServices;
using Solutio.Infrastructure.Repositories.Claims;
using Solutio.Infrastructure.Repositories.EFConfigurations.DbContexts;
using Solutio.Infrastructure.Repositories.Entities;
using Solutio.Infrastructure.Repositories.Location;
using Solutio.Infrastructure.Repositories.Mappers;
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

            //Configure urls
            services.Configure<UrlLoginSettings>(Configuration.GetSection("UrlLoginSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//.AddJwtBearer();
            .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "yourdomain.com",
                     ValidAudience = "yourdomain.com",
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKeyValue"])),
                     ClockSkew = TimeSpan.Zero
                 });

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
            services.AddTransient<IClaimFileRepository, ClaimFileRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IProvinceRepository, ProvinceRepository>();
            services.AddTransient<IClaimStateConfigurationRepository, ClaimStateConfigurationRepository>();
            services.AddTransient<IClaimThirdInsuredVehicleRepository, ClaimThirdInsuredVehicleRepository>();
            services.AddTransient<IClaimThirdInsuredPersonRepository, ClaimThirdInsuredPersonRepository>();
            services.AddTransient<IClaimInsuredVehicleRepository, ClaimInsuredVehicleRepository>();
            services.AddTransient<IClaimInsuredPersonRepository, ClaimInsuredPersonRepository>();
            services.AddTransient<IClaimAdressRepository, ClaimAdressRepository>();

            #endregion Repositories Settings

            #region Mappers Settings

            services.AddTransient<IClaimDtoMapper, ClaimDtoMapper>();
            services.AddTransient<IClaimMapper, ClaimMapper>();
            services.AddTransient<IClaimStateConfigurationMapper, ClaimStateConfigurationMapper>();
            services.AddTransient<IClaimStateMapper, ClaimStateMapper>();

            #endregion

            #region Services Settings

            services.AddTransient<INewClaimService, NewClaimService>();
            services.AddTransient<IClaimGetStateService, ClaimGetStateService>();
            services.AddSingleton<Solutio.Core.Services.ApplicationServices.IEmailSender, EmailSender>();
            services.AddTransient<ISendConfirmationEmailService, SendConfirmationEmailService>();
            services.AddTransient<ISendResetPasswordService, SendResetPasswordService>();
            services.AddTransient<IChangeClaimStateService, ChangeClaimStateService>();
            services.AddTransient<IUploadFileService, UploadFileService>();
            services.AddTransient<IDeleteFileService, DeleteFileService>();
            services.AddTransient<IGetFileService, GetFileService>();
            services.AddTransient<IGetClaimService, GetClaimService>();
            services.AddTransient<IUpdateClaimService, UpdateClaimService>();
            services.AddTransient<IDeleteClaimService, DeleteClaimService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IProvinceService, ProvinceService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<IGetClaimStateConfigurationService, GetClaimStateConfigurationService>();
         
            #endregion Services Settings

            #region Builder Settings

            services.AddTransient<ITokenBuilder, TokenBuilder>();

            #endregion Builder Settings

            #region Factories Settings

            services.AddTransient<IClaimStateFactory, ClaimStateFactory>();

            #endregion Factories Settings
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
                swagger.AddSecurityDefinition("Bearer",
                                    new ApiKeyScheme
                                    {
                                        In = "header",
                                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                                        Name = "Authorization",
                                        Type = "apiKey"
                                    });
                swagger.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {{ "Bearer", Enumerable.Empty<string>()},});
            });
        }
    }
}
