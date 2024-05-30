namespace Zeiterfassungssoftware.SharedData.Time
{
    public class TimeEntry
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TimeSpan Time => (End != null ? End - Start : DateTime.Now-Start).Value;

        public string GetTimeString()
        {
            return $"{Time.Hours}h {Time.Minutes}min";
        }

        public string GetStartString()
        {
            return $"{Start.Hour}:{Start.Minute}";
        }

        public string GetEndString()
        {
            if (End == null)
                return "?";

            return $"{End?.Hour}:{End?.Minute}";
        }


        public string GetDateString()
        {
            return $"{Start.Day}.{Start.Month}.{Start.Year}";
        }

    }
}
