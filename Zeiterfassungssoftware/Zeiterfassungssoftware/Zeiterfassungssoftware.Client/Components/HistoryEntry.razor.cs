using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class HistoryEntry
    {
        public delegate void OnButtonClick();
        
        [Parameter]
        public TimeEntry Entry { get; set; } = new()
        {

        };

        [Parameter]
        public EventCallback<OnButtonClick> OnClick { get; set; }

        [Parameter]
        public bool ShowNames { get; set; }

        private void OnEntryClicked()
        {
            OnClick.InvokeAsync();
        }
    }
}