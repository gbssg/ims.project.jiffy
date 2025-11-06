using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Globalization;
using Zeiterfassungssoftware.Client.Data.Filter;
using Zeiterfassungssoftware.SharedData.Times;
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
        public int LastCount { get; set; }


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
            LastCount = 0;
            Timer = new Timer(UpdateTimer, null, 0, 100);
        }

        protected void CalculateStats()
        {
            var Days = TimeEntrySource.GetEntries()
                                       .GroupBy(e => e.Start.Date);

            
            foreach(var Day in Days)
            {
                var CurrentDay = Day.FirstOrDefault();

                if(CurrentDay.Sick)
                {
                    Sickdays++;
                    continue;
                }
                
                foreach (var Entry in Day)
                {
                    Overtime += Entry.Time;
                }
                Overtime -= CurrentDay.ShouldTime;
            }
        }

        public void UpdateTimer(object? State)
        {
            InvokeAsync(StateHasChanged);

            if(TimeEntrySource.GetEntries().Count > LastCount)
            {
                CalculateStats();
                LastCount += 30;
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

        public bool DoFiltersApply(TimeEntryDto Entry)
		{
			return Filters[0].MatchesCriteria(Entry.Start) && Filters[1].MatchesCriteria(Entry.Start) &&
				        Filters[2].MatchesCriteria(Entry.End ?? DateTime.Now) && Filters[3].MatchesCriteria(Entry.Title) &&
				        Filters[4].MatchesCriteria(Entry.Description) && (Filters[5].MatchesCriteria(Entry.Username));
		}

        
    }
}