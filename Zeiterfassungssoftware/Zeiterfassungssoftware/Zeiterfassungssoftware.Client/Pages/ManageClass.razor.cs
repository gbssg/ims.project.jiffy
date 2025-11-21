using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.Times;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class ManageClass : ComponentBase, IDisposable
    {
        [Inject]
        public IClassProvider ClassSource { get; set; }

        [Inject]
        public IUserProvider UserSource { get; set; }

        [Inject]
        public ITimeEntryProvider TimeEntrySource { get; set; }

        public Timer? Timer { get; set; }
        public Guid SelectedClass { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Timer = new Timer(UpdateTimer, null, 0, 200);
            var Class = await ClassSource.GetOwnClass();
            SelectedClass = Class.Id;
        }

        public void UpdateClicked()
        {
            //UserSource.UpdateUser(USer.Id, SelectedClass);
        }

        public void UpdateTimer(object? State)
        {
            InvokeAsync(StateHasChanged);

            if(ClassSource.IsLoaded)
                Timer?.Dispose();
        }

        void IDisposable.Dispose()
        {
            Timer?.Dispose();
        }

    }
}