using EventBus.Messages.Common;
using MassTransit;
using Ordering.API.Consumers;
using Ordering.Infrastructure.Extensions;
using System.Reflection;

namespace Ordering.API.Extensions
{
    public static class ApiServiceRegistration
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services)
        {
            services.AddCqrsBus();
            return services;
        }

        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            services.AddScoped<BasketCheckoutConsumer>();
            return services;
        }

        public static IServiceCollection AddMassTransitConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumer<BasketCheckoutConsumer>();
                config.UsingRabbitMq((ctx, config) =>
                {
                    config.Host(configuration["EventBusSettings:RabbitMQ"], h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    config.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                    {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });
            return services;
        }
    }
}
