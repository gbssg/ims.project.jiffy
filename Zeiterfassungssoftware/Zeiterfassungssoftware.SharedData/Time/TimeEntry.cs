namespace Zeiterfassungssoftware.SharedData.Time
{
    public class TimeEntry
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TimeSpan Time => (End != null ? End - Start : TimeSpan.Zero).Value;

        public string GetTimeString()
        {
            return $"{Time.Hours}h {Time.Minutes}min";
        }

        public string GetFromToString()
        {
            if (End == null)
            {
                return $"{Start.Hour}:{Start.Minute} - ?";
            }
            return $"{Start.Hour}:{Start.Minute} - {End.Value.Hour}:{End.Value.Minute}";
        }

        public string GetDateString()
        {
            return $"{Start.Day}.{Start.Month}.{Start.Year}";
        }

    }
}
