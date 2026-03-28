using BulletinBoard.Core.Application.Interfaces;
using BulletinBoard.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BulletinBoard.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddScoped<IAnnouncementService, AnnouncementService>();

            return services;
        }
    }
}
