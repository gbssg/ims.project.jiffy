namespace Zeiterfassungssoftware.Data
{


    public class TimeEntryProvider
    {
    
        // Replace later
        public List<TimeEntry> LoadTimeEntries()
        {
            List<TimeEntry> Entries = new List<TimeEntry>();

            int I = 0;
            for(int year = 2007; year <= 2024; year++)
            {
                for(int month = 1; month <= 12; month++)
                {
                    for (int day = 1; day <= 28; day++)
                    {
                        TimeEntry Entry = new TimeEntry()
                        {
                            Id = I,
                            Date = new DateOnly(year, month, day),
                            StartTime = new TimeOnly(8, 20),
                            EndTime = new TimeOnly(16, 15),
                            Title = "Arbeit",
                            Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                        };
                        Entry.Time = Entry.EndTime - Entry.StartTime;

                        Entries.Add(Entry);
                        I++;
                    }
                }
            }
            return Entries;
        }
    }
}
