using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Client.Pages;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class HistoryEntry
    {
        [Inject]
        private NavigationManager Navigation { get; set; }

        [Parameter]
        public TimeEntry Entry { get; set; } = new()
        {

        };
        [Parameter]
        public bool ShowNames { get; set; }

        public void OnClick()
        {
            Navigation.NavigateTo($"edit/{Entry.Id}");
        }

    }
}