namespace Zeiterfassungssoftware.SharedData.ShouldTimes
{
    public class ShouldTimeDto
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Should { get; set; }
    }
}
