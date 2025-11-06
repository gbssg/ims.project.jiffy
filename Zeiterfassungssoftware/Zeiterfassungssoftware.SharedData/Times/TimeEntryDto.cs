namespace Zeiterfassungssoftware.SharedData.Times
{
    public class TimeEntryDto
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
        public TimeSpan ShouldTime { get; set; }
        public TimeSpan Ovetime => Time - ShouldTime;

        public bool Sick => Title.Equals("Krank");

        
        public string GetTimeString()
        {
            if (Sick)
                return "Krank";

            var Hours = Math.Floor(Time.TotalHours);
            var Minutes = Math.Floor((Time.TotalHours - Hours) * 60);
            string TimeString = "";


            if (Hours > 0)
                TimeString += $"{Hours}h ";

            if (Minutes > 0)
                TimeString += $"{Minutes}min ";

            if (Hours == 0 && Minutes == 0)
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
