using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{

    public class LocalTimeEntryProvider: ITimeEntryProvider
    {
        private List<TimeEntry> _timeEntries = [];

        public LocalTimeEntryProvider()
        {
            int id = 0;
            for (int year = 2023; year <= 2024; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    for (int day = 1; day <= 28; day++)
                    {
                        for(int i = 0; i < 10; i++)
                        {
                            TimeEntry Entry = new TimeEntry()
                            {
                                Start = new DateTime(year, month, day, 8, 20, 0),
                                End = new DateTime(year, month, day, 16, 15, 0),
                                Title = id % 15 == 0 ? "Krank" : "Arbeit",
                                Description = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet."
                            };

                            _timeEntries.Add(Entry);
                            id++;
                        }
                    }
                }
            }
        }

        public bool IsLoaded => true;

        public void Add(TimeEntry Entry)
        {
            _timeEntries.Add(Entry);
        }

        public void Remove(TimeEntry Entry)
        {
            _timeEntries.Remove(Entry);
        }

        public List<TimeEntry> GetEntries()
        {
            return _timeEntries;
        }
    }
}
