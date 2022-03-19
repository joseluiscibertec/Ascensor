using Ascensor.WebAPI.Data.Repositories;
using Ascensor.WebAPI.DTO.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Ascensor.WebAPI.ServiceCollection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IAscensor, AscensorRepository>();

            return services;
        }
    }
}