using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.SharedData.Classes
{
    public class ClassDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ShouldTimeDto> ShouldTimes { get; set; } = new();
    }
}
