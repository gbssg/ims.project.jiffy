using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
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

            if (Entry.End is null)
                Navigation.NavigateTo("/exception?msg=Hoppa you no can edit running entry beacuse me too lazy to write 5 lines of code :(&returnUrl=history");


            EndDate = new DateOnly(Entry.End?.Year ?? 0, Entry.End?.Month ?? 0, Entry.End?.Day ?? 0);
            EndTime = new TimeOnly(Entry.End?.Hour ?? 0, Entry.End?.Minute ?? 0, Entry.End?.Second ?? 0);
            

        }

        private void UpdateEntry()
        {
            if (Entry is null)
                return;

            Entry.Start = new DateTime(StartDate, StartTime);

            Entry.End = new DateTime(EndDate ?? DateOnly.FromDateTime, EndTime);
        }

        private void SaveChanges()
        {

        }
        private void DeleteEntry()
        {

        }


    }
}