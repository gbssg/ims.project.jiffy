using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.Roles;
using Zeiterfassungssoftware.SharedData.Times;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class EditUser : ComponentBase, IDisposable
    {

        [Parameter]
        public string Id { get; set; }

        [Inject]
        public IUserProvider UserSource { get; set; }
        [Inject]
        public IClassProvider ClassSource { get; set; }

        [Inject]
        public IRoleProvider RoleProvider { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        public UserDto? User { get; set; }
        public Timer? Timer;

        public DateOnly LockoutEndDate { get; set; }
        public TimeOnly LockoutEndTime { get; set; }

        public bool DisplayDeleteModal { get; set; }
        private List<RoleDto> SelectedRoles = new();

        protected override async Task OnInitializedAsync()
        {
            if(!string.Equals(Id, Guid.Empty.ToString()))
            {
                User = await UserSource.GetUserById(Id);
                LockoutEndDate = DateOnly.FromDateTime(User.LockoutEnd);
                LockoutEndTime = TimeOnly.FromDateTime(User.LockoutEnd);
                SelectedRoles = User.Roles;
            }
            else
            {
                User = new UserDto();
            }

            Timer = new Timer(UpdateTimer, null, 0, 200);
        }

        public void UpdateTimer(object? state)
        {
            if (User is not null && ClassSource.IsLoaded)
                Timer?.Dispose();

            InvokeAsync(StateHasChanged);
        }

        void IDisposable.Dispose()
        {
            Timer?.Dispose();
        }

        private void DeleteClicked()
        {
            DisplayDeleteModal = true;
        }

        public async void DeleteUser()
        {
            if (!string.Equals(Id, Guid.Empty.ToString()))
                await UserSource.DeleteUser(Id);

            Navigation.NavigateTo("/usermanagement");
        }

        private void ToggleRole(RoleDto role)
        {
            if(SelectedRoles.Contains(role))
            {
                SelectedRoles.Remove(role);
            }
            else
            {
                SelectedRoles.Add(role);
            }
        }
        public async void SaveChanges()
        {
            if(string.Equals(Id, Guid.Empty.ToString()))
            {
                if (!User.LockoutEnabled)
                    User.LockoutEnd = DateTime.MinValue;
                else
                    User.LockoutEnd = new DateTime(LockoutEndDate, LockoutEndTime);

                await UserSource.CreateUser(User);
                Navigation.NavigateTo("/usermanagement");
            }
            else
            {
                if (!User.LockoutEnabled)
                    User.LockoutEnd = DateTime.MinValue;
                else
                    User.LockoutEnd = new DateTime(LockoutEndDate, LockoutEndTime);

                await UserSource.UpdateUser(Id, User);
                Navigation.NavigateTo("/usermanagement");
            }
        }

    }
}