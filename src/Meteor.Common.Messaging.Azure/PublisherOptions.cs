namespace Meteor.Common.Messaging.Azure;

public record PublisherOptions<TMessage>
{
    public string TopicName { get; set; } = string.Empty;

    public string SenderName { get; set; } = string.Empty;
}