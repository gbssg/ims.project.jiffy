using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.Classes;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class Classes : ComponentBase, IDisposable
    {
        [Inject]
        public IClassProvider ClassSource { get; set; }

        public Class SelectedClass { get; set; } = new();
        public Guid SelectedGuid { get; set; }

        public Timer? Timer { get; set; }

        protected override void OnInitialized()
        {
            Timer = new Timer(UpdateTimer, null, 0, 200);
        }
        public void UpdateTimer(object? obj)
        {
            InvokeAsync(StateHasChanged);
        }

        public void SelectedClassChanged(ChangeEventArgs args)
        {
            SelectedGuid = Guid.Parse(args.Value.ToString());
            SelectedClass = SelectedGuid.Equals(Guid.Empty) ? new() : ClassSource.GetClasses().FirstOrDefault(e => e.Id.Equals(SelectedGuid)) ?? new();
        }

        public void SaveClass()
        {
            if (SelectedGuid.Equals(Guid.Empty))
            {
                ClassSource.Add(SelectedClass);
                SelectedClassChanged(new ChangeEventArgs()
                {
                    Value = Guid.Empty.ToString()
                });
            }
            else
            {
                ClassSource.Update(SelectedClass);
            }
        }

        public void DeleteClass()
        {
            ClassSource.Remove(SelectedClass);

            SelectedClassChanged(new ChangeEventArgs()
            {
                Value = Guid.Empty.ToString()
            });
        }

        void IDisposable.Dispose()
        {
            Timer?.Dispose();
        }

    }
}