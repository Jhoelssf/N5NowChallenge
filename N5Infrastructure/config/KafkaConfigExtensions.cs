using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Confluent.Kafka.Admin;

namespace N5Infrastructure.config
{
    public static class KafkaConfig
    {
        public static IKafkaConfigurationExtension AddKafka(this IServiceCollection services,
              IConfiguration configuration,
              string sectionName = "Kafka")
        {
            return (IKafkaConfigurationExtension)new KafkaConfigurationExtension(services, configuration, sectionName);
        }
    }
    public class KafkaConfigurationExtension : IKafkaConfigurationExtension
    {
        private readonly ClientConfig _clientConfig;
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private readonly string _sectionName;

        public KafkaConfigurationExtension(IServiceCollection services, IConfiguration configuration, string sectionName)
        {
            _sectionName = sectionName;
            _clientConfig = new ClientConfig()
            {
                BootstrapServers = configuration[_sectionName + ":Server"]
            };
            _services = services;
            _configuration = configuration;
        }

        public IKafkaConfigurationExtension CreateTopic(int numPartitions, short replicationFactor, params string[] names)
        {
            using (IAdminClient adminClient = new AdminClientBuilder((IEnumerable<KeyValuePair<string, string>>)this._clientConfig).Build())
            {
                foreach (string name in names)
                {
                    try
                    {
                        adminClient.CreateTopicsAsync((IEnumerable<TopicSpecification>)new List<TopicSpecification>()
                            {
                              new TopicSpecification()
                              {
                                Name = name,
                                NumPartitions = numPartitions,
                                ReplicationFactor = replicationFactor
                              }
                        }).Wait();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred creating topic " + name + " : " + ex.Message);
                    }
                }
            }
            return (IKafkaConfigurationExtension)this;
        }
    }

    public interface IKafkaConfigurationExtension
    {
        IKafkaConfigurationExtension CreateTopic(
        int numPartitions,
        short replicationFactor,
        params string[] names);
    }
}