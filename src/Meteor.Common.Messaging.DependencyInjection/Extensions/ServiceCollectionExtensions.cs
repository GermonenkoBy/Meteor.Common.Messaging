using Meteor.Common.Messaging.Abstractions;
using Meteor.Common.Messaging.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Meteor.Common.Messaging.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServiceBusPublisher<TMessage>(
        this IServiceCollection services,
        Action<PublisherOptions<TMessage>> configure,
        ServiceLifetime publisherLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Singleton
    )
    {
        var optionsServiceDescriptor = new ServiceDescriptor(
            typeof(PublisherOptions<TMessage>),
            _ =>
            {
                var options = new PublisherOptions<TMessage>();
                configure(options);
                return options;
            },
            optionsLifetime
        );

        var publisherServiceDescriptor = new ServiceDescriptor(
            typeof(IPublisher<TMessage>),
            typeof(AzureServiceBusPublisher<TMessage>),
            publisherLifetime
        );

        services.TryAdd(optionsServiceDescriptor);
        services.TryAdd(publisherServiceDescriptor);
    }
}