using System.Text.Json.Serialization;

namespace Domain.Events
{
    public abstract class Event
    {
        [JsonPropertyName("id")] public Guid Id { get; } = Guid.NewGuid();
        [JsonPropertyName("ts")] public long Ticks { get; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();

        protected Event(Guid id)
        {
            Id = id;
        }
    }
}
