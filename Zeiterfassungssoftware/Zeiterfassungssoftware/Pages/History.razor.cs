using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Time;

namespace Zeiterfassungssoftware.Pages
{
    public partial class History
    {
        
        
        protected override async Task OnInitializedAsync()
        {
            if (TimeEntryProvider.TimeEntries == null)
                TimeEntryProvider.LoadTimeEntries();
        }

    }
}