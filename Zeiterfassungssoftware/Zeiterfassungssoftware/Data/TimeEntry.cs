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
        public TimeSpan Time { get; set; }

        public string GetTimeString()
        {
            return $"{Time.Hours}h {Time.Minutes}min";
        }

        public string GetFromToString()
        {
            return $"{StartTime.Hour}:{StartTime.Minute} - {EndTime.Hour}:{EndTime.Minute}";
        }

        public string GetDateString()
        {
            return $"{Date.Day}.{Date.Month}.{Date.Year}";
        }

    }
}
