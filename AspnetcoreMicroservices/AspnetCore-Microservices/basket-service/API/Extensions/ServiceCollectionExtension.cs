using API.GrpcServices;
using API.Repositories;
using API.Repositories.interfaces;
using Discount.Grpc.Protos;
using EventBus.Messages.Common;
using MassTransit;
using System.Reflection;

namespace API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static int DiscountGprcService { get; private set; }

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
            return services;
        }

        public static IServiceCollection AddAutoMapperService(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }

        public static IServiceCollection AddGrpcService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(_ => _.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
            services.AddScoped<DiscountGrpcService>();
            return services;
        }

        public static IServiceCollection AddMassTransitService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(config =>
            {
                config.UsingRabbitMq((ctx, config) =>
                {
                    config.Host(configuration["EventBusSettings:RabbitMQ"]);
                });
            });
            return services;
        }
    }
}
