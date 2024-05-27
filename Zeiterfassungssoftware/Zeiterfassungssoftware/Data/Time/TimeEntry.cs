namespace Zeiterfassungssoftware.Data.Time
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Time { get; set; }

        public string GetTimeString()
        {
            return $"{Time.Hours}h {Time.Minutes}min";
        }

        public string GetFromToString()
        {
            return $"{Start.Hour}:{Start.Minute} - {End.Hour}:{End.Minute}";
        }

        public string GetDateString()
        {
            return $"{Start.Day}.{Start.Month}.{Start.Year}";
        }

    }
}
