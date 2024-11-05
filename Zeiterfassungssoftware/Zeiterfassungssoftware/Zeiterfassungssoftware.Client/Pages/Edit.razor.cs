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
            StartDate = DateOnly.FromDateTime(Entry.Start);
            StartTime = TimeOnly.FromDateTime(Entry.Start);
            
            DateTime End = Entry.End.GetValueOrDefault(DateTime.Now);
            EndDate = DateOnly.FromDateTime(End);
            EndTime = TimeOnly.FromDateTime(End);
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
            TimeEntrySource.Update(Entry);
            Navigation.NavigateTo("/history");
        }

        private async void DeleteEntry()
        {
            if (Entry is null)
                return;

            await TimeEntrySource.Remove(Entry);

            Navigation.NavigateTo("/history");
        }


    }
}
