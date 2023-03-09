using Web.Models.Enums;

namespace Web.Models
{
    public class Archive
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public ProcessingStatus Status { get; set; }

    }
}
