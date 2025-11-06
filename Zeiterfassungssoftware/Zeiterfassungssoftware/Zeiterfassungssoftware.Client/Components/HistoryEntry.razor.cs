using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Client.Pages;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class HistoryEntry
    {
        [Inject]
        private NavigationManager Navigation { get; set; }

        [Parameter]
        public TimeEntryDto Entry { get; set; } = new();

        public void OnClick()
        {
            Navigation.NavigateTo($"edit/{Entry.Id}");
        }

    }
}