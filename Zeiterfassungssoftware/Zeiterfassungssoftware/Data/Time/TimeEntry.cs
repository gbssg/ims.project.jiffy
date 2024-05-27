namespace Zeiterfassungssoftware.Data.Time
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
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
