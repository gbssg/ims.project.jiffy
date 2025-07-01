using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using Zeiterfassungssoftware.Client.Data.Filter;
using Zeiterfassungssoftware.SharedData.Time;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class History : IDisposable
    {
        [Inject]
        public ITimeEntryProvider TimeEntrySource { get; set; }
        
        [Inject]
        private NavigationManager Navigation { get; set; }

        public Timer? Timer { get; set; }

        public int Sickdays { get; set; } = 0;
        public TimeSpan Overtime { get; set; } = new TimeSpan(0, 0, 0);
        public readonly TimeSpan NeededWeeklyTime = new TimeSpan(14, 0, 0);
        
        public bool ShowFilters { get; set; }
        public bool Loaded { get; set; }

        [Inject]
        private IJSRuntime js { get; set; }


        private IFilter[] Filters =
        [
            new DateFilter("Date"),
            new TimeFilter("Start Time"),
            new TimeFilter("Stop Time"),
            new StringFilter("Activity"),
            new StringFilter("Description"),
            new StringFilter("Username"),
        ];
        private IFilter SelectedFilter => Filters.FirstOrDefault(e => e.PopUp);

        

        protected override void OnInitialized()
        {
            Timer = new Timer(UpdateTimer, null, 0, 100);
        }

        protected void CalculateStats()
        {
            var Weeks = TimeEntrySource.GetEntries()
                                       .GroupBy(e => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(e.Start, CalendarWeekRule.FirstDay, DayOfWeek.Monday));

            foreach (var Week in Weeks)
            {
                foreach (var Entry in Week)
                {
                    if (Entry.Sick)
                        Sickdays++;
                    else
                        Overtime += Entry.Time;
                }
                Overtime -= NeededWeeklyTime;
            }
        }

        public void UpdateTimer(object? State)
        {
            InvokeAsync(StateHasChanged);

            if (TimeEntrySource.GetEntries().Any() && !Loaded)
            {
                CalculateStats();
                Loaded = true;
            }
        }

        public void OnFilterClicked(IFilter Filter)
        {
            if (SelectedFilter is null)
                Filter.PopUp = true;
        }

        void IDisposable.Dispose()
        { 
            Timer?.Dispose();
        }

        public bool DoFiltersApply(TimeEntry Entry)
		{
			return Filters[0].MatchesCriteria(Entry.Start) && Filters[1].MatchesCriteria(Entry.Start) &&
				        Filters[2].MatchesCriteria(Entry.End ?? DateTime.Now) && Filters[3].MatchesCriteria(Entry.Title) &&
				        Filters[4].MatchesCriteria(Entry.Description) && (Filters[5].MatchesCriteria(Entry.Username));
		}

        
    }
}