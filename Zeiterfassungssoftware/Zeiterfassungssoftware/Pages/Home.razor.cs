using Zeiterfassungssoftware.Data.Time;

namespace Zeiterfassungssoftware.Pages
{
    public partial class Home
    {
        protected override async Task OnInitializedAsync()
        {
            if(TimeEntryProvider.TimeEntries == null)
                TimeEntryProvider.LoadTimeEntries();
            
        }
    }
}