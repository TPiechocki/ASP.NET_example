using System.ComponentModel.DataAnnotations;
using Example.WebApi.Context;
using Example.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NSwag.Generation.Processors;
using System.Text;
using Example.WebApi.Config;

namespace Example.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Config
            services.Configure<ExampleConfig>(exampleConfig =>
            {
                Configuration.Bind(ExampleConfig.ConfigurationPrefix, exampleConfig);
                Validator.ValidateObject(exampleConfig, new ValidationContext(exampleConfig), true);
            });
            services.AddSingleton<IExampleConfig, ExampleConfig>();

            // Database
            services.AddDbContext<ExampleDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ExampleConnectionString")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            // CORS
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            // auth with JWT token
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "https://localhost:5001",
                        ValidAudience = "https://localhost:5001",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            Configuration.GetValue<string>($"{ExampleConfig.ConfigurationPrefix}:JwtSigningKey")))
                    };
                });

            // add API versioning
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            });
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // add openApi documents for each version
            foreach (var versionInfo in Versions)
            {
                services.AddOpenApiDocument(doc =>
                {
                    var apiVersionProcessor =
                        new ApiVersionProcessor { IncludedVersions = new[] { $"v{versionInfo.Version}" } };
                    //Add processor first in line to allow it exclude other version DTO schemas.
                    doc.OperationProcessors.Insert(0, apiVersionProcessor);

                    doc.DocumentName = $"v{versionInfo.Version}";
                    doc.Description = versionInfo.Description;
                    doc.Version = versionInfo.Version;
                    doc.Title = versionInfo.Title;
                });
            }


            // DI
            services.AddScoped<IExampleDbContext, ExampleDbContext>()
                .AddScoped<ISatelliteService, SatellitesService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthService, AuthService>();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static OpenApiInfo[] Versions { get; } =
        {
            CreateSwaggerInfo("1"), CreateSwaggerInfo("2")
        };

        private static OpenApiInfo CreateSwaggerInfo(string apiVersion)
        {
            return new OpenApiInfo
            {
                Title = $"Example API v{apiVersion}",
                Description = "Provides example functionality",
                Version = apiVersion
            };
        }
    }
}
