using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Zeiterfassungssoftware.Client.Data.Filter;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class History : IDisposable
    {
        [Inject]
        public ITimeEntryProvider TimeEntrySource { get; set; }

        public Timer? Timer { get; set; }
        public int LastCount { get; set; }

        public bool ShowFilters { get; set; }
        public TimeSpan Overtime { get; set; } = new TimeSpan(0, 0, 0);


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

        private IEnumerable<TimeEntryDto> FilteredEntries =>
            TimeEntrySource.GetEntries()
                .Where(DoFiltersApply)
                .Take(LastCount);

        private string OvertimeDisplay =>
            Math.Abs(Overtime.TotalHours) < 1
                ? $"{Math.Floor(Overtime.TotalMinutes)}min"
                : $"{Math.Floor(Overtime.TotalHours)}h";

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

                foreach (var Entry in Day)
                    Overtime += Entry.Time;
                
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


        public bool DoFiltersApply(TimeEntryDto Entry)
		{
			return Filters[0].MatchesCriteria(Entry.Start) && 
                   Filters[1].MatchesCriteria(Entry.Start) &&
				   Filters[2].MatchesCriteria(Entry.End ?? DateTime.Now) && 
                   Filters[3].MatchesCriteria(Entry.Title) &&
				   Filters[4].MatchesCriteria(Entry.Description) && 
                   Filters[5].MatchesCriteria(Entry.Username);
		}

        void IDisposable.Dispose()
        { 
            Timer?.Dispose();
        }
        
    }
}