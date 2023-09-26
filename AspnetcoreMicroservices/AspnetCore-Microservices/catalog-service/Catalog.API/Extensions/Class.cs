using Catalog.API.Data;
using Catalog.API.Repositories;

namespace Catalog.API.Extensions
{
    public static class CatalogServiceCollection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }

        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddScoped<ICatalogContext, CatalogContext>();
            return services;
        }
    }
}
