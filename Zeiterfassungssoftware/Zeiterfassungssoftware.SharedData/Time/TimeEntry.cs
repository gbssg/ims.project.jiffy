namespace Zeiterfassungssoftware.SharedData.Time
{
    public class TimeEntry
    {
        private const string TIME_FORMAT = "HH:mm";
        //private const string DURATION_FORMAT = @"h\h\ m\m\i\n\ s\s";
        private const string DATE_FORMAT = "dd MMM yyyy";

        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public TimeSpan Time => (End != null ? End - Start : DateTime.Now-Start).Value;
        public bool Sick => Title.Equals("Krank");

        public bool IsWeekend => (Start.DayOfWeek == DayOfWeek.Sunday || Start.DayOfWeek == DayOfWeek.Saturday);

        public string GetTimeString()
        {
            if (Sick)
                return "Krank";

            string TimeString = "";

            if (Time.Hours > 0)
                TimeString += $"{Time.Hours}h ";

            if (Time.Minutes > 0)
                TimeString += $"{Time.Minutes}min ";

            TimeString += $"{Time.Seconds}s";

            return TimeString;
        }

        public string GetStartString()
        {
            return Sick ? "" : Start.ToString(TIME_FORMAT);
        }

        public string GetEndString()
        {
            return Sick ? "" : End?.ToString(TIME_FORMAT) ?? "?";
        }


        public string GetDateString()
        {
            return Start.ToString(DATE_FORMAT);
        }

    }
}
