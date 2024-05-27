namespace Zeiterfassungssoftware.Data.Time
{


    public class TimeEntryProvider
    {

        // Replace later
        public List<TimeEntry> LoadTimeEntries()
        {
            List<TimeEntry> Entries = new List<TimeEntry>();

            int I = 0;
            for (int year = 2021; year <= 2024; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    for (int day = 1; day <= 28; day++)
                    {
                        TimeEntry Entry = new TimeEntry()
                        {
                            Id = I,
                            Start = new DateTime(year, month, day, 8, 20, 0),
							End = new DateTime(year, month, day, 16, 15, 0),
							Title = I % 15 == 0 ? "Krank" : "Arbeit",
                            Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                        };
                        Entry.Time = Entry.End - Entry.Start;

                        Entries.Add(Entry);
                        I++;
                    }
                }
            }
            return Entries;
        }
    }
}
