using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Persistences;
using Ordering.Application.Contracts.Persistences.Base;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Persistence.Repositories;
using Ordering.Infrastructure.Persistence.Repositories.Base;
using Ordering.Infrastructure.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Infrastructure.UnitOfWork;
using Ordering.Infrastructure.CQRSBus.Interfaces;
using Ordering.Infrastructure.CQRSBus;

namespace Ordering.Infrastructure.Extensions
{
    public static class InfrastrutureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<OrderContext>();
            }

            using (var context = serviceProvider.GetService<OrderContext>())
            {
                context!.Database.Migrate();
            }

            return services;
        }

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfwork, Ordering.Infrastructure.UnitOfWork.UnitOfWork>();
            return services;
        }
        
        public static IServiceCollection AddCqrsBus(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();
            return services;
        }
    }
}
