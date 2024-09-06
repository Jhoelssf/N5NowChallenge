namespace N5Application.Config;

public class KafkaConfigurationLayer
{
    public string Server { get; init; }
    public string TopicPermissions { get; init; }

    public KafkaConfigurationLayer(string server, string topicPermissions)
    {
        Server = server ?? throw new ArgumentNullException(nameof(server));
        TopicPermissions = topicPermissions ?? throw new ArgumentNullException(nameof(topicPermissions));
    }
}