using Microsoft.Extensions.Configuration;

namespace N5Infrastructure.KafkaConfig;
public class KafkaConfiguratorProvider : IKafkaConfigurationProvider
{
    private readonly IConfiguration _configuration;

    public KafkaConfiguratorProvider(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string Server => _configuration["Kafka:Server"]!;

    public string TopicPermissions => _configuration["Topics:Permissions"]!;
}