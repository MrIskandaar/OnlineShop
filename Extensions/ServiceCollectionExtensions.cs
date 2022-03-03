using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Root.Helpers;

namespace Root.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// registers postgre DbContext as a service to IServerCollection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            ShopContext.ConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ShopContext>(options => options.UseNpgsql(ShopContext.ConnectionString));
        }

        /// <summary>
        /// adds swagger generator to service
        /// </summary>
        /// <param name="services"></param>
        public static void SetSwaggerGen(this IServiceCollection services, string? assemblyName)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new()
                {
                    Version = "v1",
                    Title = "Online Shop",
                    Description = "Software to online shopping"
                });
                s.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
                {
                    Description = "JWT Bearer: \"Authorization: Bearer { token }\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    In = ParameterLocation.Header
                });
                s.AddSecurityRequirement(new()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        Array.Empty<string>()
                    }
                });
                var xmlFile = $"{assemblyName}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// enables jwt-bearer authentication 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureJwtAuthService(this IServiceCollection services)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = AuthorizationHelper.ISSUER,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthorizationHelper.GetSecurityKey(),

                ValidateAudience = true,
                ValidAudience = AuthorizationHelper.AUDIENCE,

                ValidateLifetime = true
            };
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.SaveToken = true;
                    o.TokenValidationParameters = tokenValidationParameters;
                    o.RequireHttpsMetadata = false;
                });
        }

        /// <summary>
        /// sets cross-origin resource sharing services
        /// we cannot use AllowAnyCredentioals and AllowAnyOrigin at the same time
        /// </summary>
        /// <param name="services"></param>
        public static void SetCors(this IServiceCollection services)
        {
            services.AddCors(
                options => options.AddPolicy("Allow Cors",
                    builder =>
                    {
                        builder.AllowAnyMethod()
                               .AllowAnyOrigin();
                    }));
        }
    }
}
