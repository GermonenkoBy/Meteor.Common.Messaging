namespace Meteor.Common.Messaging.Abstractions;

public interface IPublisher<TMessage>
{
    Task PublishAsync(TMessage body, string? messageId = null, DateTimeOffset? scheduleTime = null);
}