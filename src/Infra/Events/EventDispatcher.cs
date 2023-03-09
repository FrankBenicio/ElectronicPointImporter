using Azure.Messaging.ServiceBus;
using Domain.Events.Interfaces;
using Domain.Events;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Events
{
    [ExcludeFromCodeCoverage]
    public abstract class EventDispatcher<T> : IEventDispatcher<T> where T : Event
    {
        private readonly ServiceBusSender _sender;

        protected EventDispatcher(ServiceBusClient serviceBusClient, string queueOrTopicName)
        {
            if (serviceBusClient == null)
                throw new ArgumentNullException(nameof(serviceBusClient));

            if (string.IsNullOrWhiteSpace(queueOrTopicName))
                throw new ArgumentNullException(nameof(queueOrTopicName));

            _sender = serviceBusClient.CreateSender(queueOrTopicName);
        }

        public virtual async Task Dispatch(T @event, Dictionary<string, object> properties = null)
        {
            var message = new ServiceBusMessage(JsonSerializer.Serialize(@event));

            if (properties != null)
            {
                foreach (var property in properties)
                {
                    message.ApplicationProperties.Add(property.Key, property.Value);
                }
            }

            await _sender.SendMessageAsync(message).ConfigureAwait(false);
        }
    }
}
