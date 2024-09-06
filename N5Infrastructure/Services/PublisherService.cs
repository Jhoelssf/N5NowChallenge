using Confluent.Kafka;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Application.Common.Services;
using N5Application.Config;
using N5Application.DTO;

namespace Infrastructure.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly KafkaConfigurationLayer _kafkaConfiguration;

        public PublisherService(KafkaConfigurationLayer kafkaConfiguration)
        {
            _kafkaConfiguration = kafkaConfiguration ?? throw new ArgumentNullException(nameof(kafkaConfiguration));

            var config = new ProducerConfig()
            {
                BootstrapServers = _kafkaConfiguration.Server
            };

            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task Publish(PublishDto publishDto, CancellationToken cancellationToken)
        {
            try
            {
                var value = JsonSerializer.Serialize(publishDto, new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() }
                });

                await _producer.ProduceAsync(_kafkaConfiguration.TopicPermissions, new Message<Null, string>()
                {
                    Value = value
                }, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}