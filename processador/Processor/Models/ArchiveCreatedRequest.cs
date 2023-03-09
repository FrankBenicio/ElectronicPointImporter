using System.Text.Json.Serialization;

namespace Processor.Models
{
    public class ArchiveCreatedRequest
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("ts")]
        public long Ticks { get; } = DateTime.UtcNow.Ticks;
    }
}
