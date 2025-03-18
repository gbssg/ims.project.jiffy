

using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class ManageActivities : IDisposable
    {
        
        private string ActivityTitle { get; set; } = string.Empty;
        private string ActivityDescription { get; set; } = string.Empty;
        private ActivityTitle SelectedTitle => ActivitySource.GetActivityTitles().FirstOrDefault(e => e.Value == ActivityTitle) ?? new ActivityTitle();
        private ActivityDescription SelectedDescription => ActivitySource.GetActivityDescriptions().FirstOrDefault(e => e.Value == ActivityDescription) ?? new ActivityDescription();

        private Timer? Timer { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Timer = new Timer(UpdateTimer, null, 0, 200);
        }

        public void UpdateTimer(object? state)
        {
            if (ActivitySource.IsLoaded)
                Timer?.Dispose();
            
            InvokeAsync(StateHasChanged);
        }


        void IDisposable.Dispose()
        {
            Timer?.Dispose();
        }
    }
}