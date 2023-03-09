using Azure.Messaging.ServiceBus;
using Domain.Events;
using System.Diagnostics.CodeAnalysis;

namespace Infra.Events
{
    [ExcludeFromCodeCoverage]
    public class ArchiveCreatedDispatcher : EventDispatcher<ArchiveCreated>
    {
        public ArchiveCreatedDispatcher(ServiceBusClient serviceBusClient) : base(serviceBusClient, "sbt-archive-created")
        {
        }
    }
}
