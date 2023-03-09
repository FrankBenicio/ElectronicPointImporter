namespace Domain.Events.Interfaces
{
    public interface IEventDispatcher<in T> where T : Event
    {
        Task Dispatch(T @event, Dictionary<string, object> properties = null);
    }
}
