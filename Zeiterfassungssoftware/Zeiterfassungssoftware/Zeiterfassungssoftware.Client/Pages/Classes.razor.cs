using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class Classes : ComponentBase, IDisposable
    {
        [Inject]
        public IClassProvider ClassSource { get; set; }
        [Inject]
        public IShouldTimeProvider ShouldTimeSource { get; set; }

        public ClassDto SelectedClass { get; set; } = new();
        public Guid SelectedGuid { get; set; }

        public List<ShouldTimeDto> Original { get; set; } = new();

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
            Original = new(SelectedClass.ShouldTimes);
        }

        public async Task SaveClass()
        {
            if (SelectedGuid == Guid.Empty)
            {
                await ClassSource.CreateClass(SelectedClass);
                SelectedClassChanged(new ChangeEventArgs { Value = Guid.Empty.ToString() });
            }
            else
            {
                foreach (var ShouldTime in Original)
                {
                    await ShouldTimeSource.DeleteShouldTime(ShouldTime.Id);
                }

                for (int i = 0; i < SelectedClass.ShouldTimes.Count; i++)
                {
                    var ShouldTime = SelectedClass.ShouldTimes[i];
                    ShouldTime.ClassId = SelectedClass.Id;
                    var ConfirmedShouldTime = await ShouldTimeSource.CreateShouldTime(ShouldTime);
                    SelectedClass.ShouldTimes[i] = ConfirmedShouldTime;
                }
                SelectedClass = await ClassSource.UpdateClass(SelectedClass.Id, SelectedClass);
                Original = new(SelectedClass.ShouldTimes);
            }
        }

        public void DeleteClass()
        {

            ClassSource.DeleteClass(SelectedClass.Id);

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