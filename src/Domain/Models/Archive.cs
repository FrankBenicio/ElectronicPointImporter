using Domain.Enums;

namespace Domain.Models
{
    public class Archive
    {
        public Archive(string name)
        {
            Name = name;
            Directory = Guid.NewGuid().ToString();
            Status = ProcessingStatus.PENDING;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Directory { get; private set; }
        public ProcessingStatus Status { get; private set; }


        public void Process()
        {
            Status = ProcessingStatus.PROCESSING;
        }

        public void Finalize()
        {
            Status = ProcessingStatus.FINISHED;
        }

    }
}
