using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Time;

namespace Zeiterfassungssoftware.Pages
{
    public partial class History
    {

        public List<TimeEntry> Entries;

        protected override async Task OnInitializedAsync()
        {
            Entries = new TimeEntryProvider().LoadTimeEntries();
        }

    }
}