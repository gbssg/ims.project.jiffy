namespace Zeiterfassungssoftware.Data
{
    public class TimeEntry
    {
        public int Id { get; init; }
        public DateOnly Date { get; init; }
        public TimeOnly StartTime { get; init; }
        public TimeOnly EndTime { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
    }
}
