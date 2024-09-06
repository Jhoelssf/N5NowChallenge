using Application.Common.Services;
using Confluent.Kafka;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace N5Infrastructure.config
{
    public static class ApacheKafkaConfig
    {
        public static IServiceCollection AddApacheKafka(this IServiceCollection services, IConfiguration configuration)
        {
            var kafkaConnection = configuration["Kafka:Server"];
            services.AddSingleton<ProducerConfig>(provider => new ProducerConfig
            {
                BootstrapServers = kafkaConnection,
                ClientId = "n5now-producer",
            });

            services.AddSingleton<ConsumerConfig>(provider => new ConsumerConfig
            {
                BootstrapServers = kafkaConnection,
                GroupId = "n5now-consumer-group",
            });
            return services;
        }

    }
}