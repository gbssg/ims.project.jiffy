using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class UserManagement : ComponentBase
    {

        [Inject]
        public IUserProvider UserProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        private Timer? Timer;

        protected override void OnInitialized()
        {
            Timer = new Timer(UpdateTimer, null, 0, 1000);
        }


        public void UpdateTimer(object? obj)
        {
            InvokeAsync(StateHasChanged);
        }

        public void OnUserClicked(UserDto user)
        {
            Navigation.NavigateTo($"user/{user.Id}");
        }
    
    }
}
