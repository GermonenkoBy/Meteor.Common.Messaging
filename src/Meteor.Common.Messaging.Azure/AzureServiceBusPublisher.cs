using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Meteor.Common.Messaging.Abstractions;

namespace Meteor.Common.Messaging.Azure;

public class AzureServiceBusPublisher<TMessage> : IPublisher<TMessage>, IAsyncDisposable
{
    private readonly ServiceBusClient _serviceBusClient;

    private readonly PublisherOptions<TMessage> _options;

    private ServiceBusSender? _sender;

    private ServiceBusSender Sender =>
        _sender
            ??= _serviceBusClient.CreateSender(
                    _options.TopicName,
                    new()
                    {
                        Identifier = _options.SenderName
                    }
                );

    public AzureServiceBusPublisher(ServiceBusClient serviceBusClient, PublisherOptions<TMessage> options)
    {
        _serviceBusClient = serviceBusClient;
        _options = options;
    }

    public async Task PublishAsync(
        TMessage body,
        string? messageId = null,
        DateTimeOffset? scheduleTime = null
    )
    {
        var bodyBytes = JsonSerializer.SerializeToUtf8Bytes(body);
        var message = new ServiceBusMessage(bodyBytes);

        if (messageId is not null)
        {
            message.MessageId = messageId;
        }

        if (scheduleTime.HasValue)
        {
            message.ScheduledEnqueueTime = scheduleTime.Value;
        }

        await Sender.SendMessageAsync(message);
    }

    public async ValueTask DisposeAsync()
    {
        if (_sender is not null)
        {
            await _sender.DisposeAsync();
        }
    }
}