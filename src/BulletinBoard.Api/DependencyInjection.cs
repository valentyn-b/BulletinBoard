using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace BulletinBoard.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new System.Text.Json.Serialization.JsonStringEnumConverter());
                });

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidAudience = jwtSettings["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                    };
                });

            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddSwaggerWithXml(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFileApi = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPathApi = Path.Combine(AppContext.BaseDirectory, xmlFileApi);
                if (File.Exists(xmlPathApi)) c.IncludeXmlComments(xmlPathApi);

                var xmlFileCore = "BulletinBoard.Core.xml";
                var xmlPathCore = Path.Combine(AppContext.BaseDirectory, xmlFileCore);
                if (File.Exists(xmlPathCore)) c.IncludeXmlComments(xmlPathCore);
            });

            return services;
        }
    }
}
