using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Time;

namespace Zeiterfassungssoftware.Pages
{
    public partial class History
    {
        public List<TimeEntry> Entries;
        public int SickDays;
        
        protected override async Task OnInitializedAsync()
        {
            Entries = new TimeEntryProvider().LoadTimeEntries();
            SickDays = Entries.Where(e => e.Title.ToLower().Trim().Equals("krank")).Count();

            
        }

    }
}