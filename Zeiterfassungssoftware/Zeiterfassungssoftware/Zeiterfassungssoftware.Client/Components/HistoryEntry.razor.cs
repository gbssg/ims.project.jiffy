using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class HistoryEntry
    {
        [Parameter]
        public TimeEntry Entry { get; set; } = new()
        {

        };
    }
}