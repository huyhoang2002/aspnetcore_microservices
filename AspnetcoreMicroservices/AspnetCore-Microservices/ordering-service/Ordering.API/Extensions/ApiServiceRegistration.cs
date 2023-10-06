using Ordering.Infrastructure.Extensions;

namespace Ordering.API.Extensions
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddCqrsBus();
            return services;
        }
    }
}
