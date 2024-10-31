using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Web;
using Zeiterfassungssoftware.Client.Services;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class Edit
    {
        public readonly string TIME_FORMAT = @"HH\:mm\.ss dd\.MM\.yyyy";

        [Parameter]
        public Guid Id { get; set; }

        [Inject]
        public ITimeEntryProvider TimeEntrySource { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        public TimeEntry? Entry { get; set; }
        public DateOnly StartDate { get; set; }
        public TimeOnly StartTime { get; set; }
        public DateOnly? EndDate { get; set; }
        public TimeOnly? EndTime { get; set; }


        public Timer? Timer { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Entry = await TimeEntrySource.GetEntryById(Id);
            StartDate = new DateOnly(Entry.Start.Year, Entry.Start.Month, Entry.Start.Day);
            StartTime = new TimeOnly(Entry.Start.Hour, Entry.Start.Minute, Entry.Start.Second);

            DateTime End = Entry.End.GetValueOrDefault(DateTime.Now);
            EndDate = new DateOnly(End.Year, End.Month, End.Day);
            EndTime = new TimeOnly(End.Hour, End.Minute, End.Second);
        }

        private void UpdateEntry()
        {
            if (Entry is null)
                return;

            Entry.Start = new DateTime(StartDate, StartTime);

            if (EndDate is null || EndTime is null)
                Entry.End = DateTime.Now;
            else
                Entry.End = EndDate.Value.ToDateTime(EndTime.Value);
        }

        private void SaveChanges()
        {
            UpdateEntry();
        }

        private void DeleteEntry()
        {
            if (Entry is null)
                return;

            var task = Task.Run(async () => await TimeEntrySource.Remove(Entry));
            task.GetAwaiter().GetResult();


            Navigation.NavigateTo("/history");
            
        }


    }
}
