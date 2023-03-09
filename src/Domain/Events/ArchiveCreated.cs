
namespace Domain.Events
{
    public class ArchiveCreated : Event
    {
        public ArchiveCreated(Guid id) : base(id)
        {
        }
    }
}
