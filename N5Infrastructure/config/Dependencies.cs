using Application.Common.Services;
using Infrastructure.Persistance.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5Application.Config;
using N5Domain.Repositories;
using N5Infrastructure.Data;
using N5Infrastructure.Repository;
using Nest;

namespace N5Infrastructure.config
{
    public static class Dependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringElastic = configuration["ElasticConfiguration:Uri"] ?? throw new ArgumentNullException("ConnectionUriElastic");
            var defaultIndexElastic = configuration["ElasticConfiguration:N5Index"] ?? throw new ArgumentNullException("ElasticIndexName");

            services.AddSingleton<IElasticClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();

                var settings = new ConnectionSettings(new Uri(connectionStringElastic))
                    .DefaultIndex(defaultIndexElastic);

                return new ElasticClient(settings);
            });

            var cs = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(cs));

            services.AddScoped<ApplicationDbContext>();

            var kafkaConfiguration = new KafkaConfigurationLayer(
                configuration["Kafka:Server"],
                configuration["Topics:Permissions"]
            );
            services.AddSingleton(kafkaConfiguration);


            services.AddApacheKafka(configuration);

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<IElasticProducer, ElasticProducer>();

            return services;
        }
    }
}

