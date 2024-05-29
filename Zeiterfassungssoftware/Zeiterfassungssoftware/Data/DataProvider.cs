using Zeiterfassungssoftware.Data.Activities;
using Zeiterfassungssoftware.Data.Holidays;
using Zeiterfassungssoftware.Data.Time;

namespace Zeiterfassungssoftware.Data
{
    public class DataProvider
    {
        public static void LoadData()
        {
            LoadHolidays();
            LoadTimeEntries();
            LoadActivities();
        }

        public static void LoadHolidays()
        {
            if (HolidayProvider.Holidays == null)
                HolidayProvider.LoadHolidaysAsync();
        }

        public static void LoadTimeEntries()
        {
            if (TimeEntryProvider.TimeEntries == null)
                TimeEntryProvider.LoadTimeEntries();
        }

        public static void LoadActivities()
        {
            LoadActivityTitles();
            LoadActivityDescriptions();
        }

        public static void LoadActivityTitles()
        {
            if (ActivityProvider.ActivityTitles == null)
                ActivityProvider.LoadActivityTitles();
        }

        public static void LoadActivityDescriptions()
        {
            if (ActivityProvider.ActivityDescriptions == null)
                ActivityProvider.LoadActivityDescriptions();
        }
    }
}
