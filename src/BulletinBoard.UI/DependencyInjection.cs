using BulletinBoard.UI.Auth;
using BulletinBoard.UI.Clients;
using BulletinBoard.UI.Configuration;
using BulletinBoard.UI.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

namespace BulletinBoard.UI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services, IConfiguration configuration)
        {
            var apiBaseUrl = configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(apiBaseUrl))
            {
                throw new InvalidOperationException("API BaseUrl is not configured in appsettings.json");
            }

            services
                .AddHttpClient<IAnnouncementApiClient, AnnouncementApiClient>(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                })
                .AddHttpMessageHandler<JwtAuthHeaderHandler>();

            return services;
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHttpContextAccessor()
                .AddTransient<JwtAuthHeaderHandler>()
                .Configure<JwtSettings>(configuration.GetSection("JwtSettings"))
                .AddScoped<IJwtTokenGenerator, JwtTokenGenerator>()
                .AddScoped<GoogleOAuthEvents>()
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                    {
                        options.LoginPath = "/login";
                    })
                .AddGoogle(options =>
                    {
                        options.ClientId = configuration["Authentication:Google:ClientId"]!;
                        options.ClientSecret = configuration["Authentication:Google:ClientSecret"]!;
                        options.EventsType = typeof(GoogleOAuthEvents);
                    });            

            return services;
        }
    }
}
