// Interface configuration Kafka
namespace N5Infrastructure.KafkaConfig;
public interface IKafkaConfigurationProvider
{
    public string TopicPermissions { get; }
    public string Server { get; }
}