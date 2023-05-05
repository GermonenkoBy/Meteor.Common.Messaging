using System.Text.Json;

namespace Meteor.Common.Messaging.Azure;

public record PublisherOptions<TMessage>
{
    public string TopicName { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;

    public JsonSerializerOptions SerializerOptions { get; set; } = new ()
    {
        IncludeFields = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
}