using Microsoft.AspNetCore.Components.Web;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Filter;
using Zeiterfassungssoftware.Services;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Pages
{
    public partial class History
    {
        //test
        public TimeEntry[] TimeEntries { get; set; } = [];

        public int SickDays => TimeEntrySource.GetEntries().Where(e => e.Title.ToLower().Trim().Equals("krank")).Count();
        public TimeSpan Overtime = new TimeSpan(0, 0, 0);
        public TimeSpan NeededDailyTime = new TimeSpan(6, 30, 0);

        private IFilter[] Filters =
        {
            new DateFilter("Date"),
            new TimeFilter("Start Time"),
            new TimeFilter("Stop Time"),
            new StringFilter("Activity"),
            new StringFilter("Description"),
        };

        public Timer? RefreshTimer;
        public int SearchResults => TimeEntries.Where(e => DoFiltersApply(e)).Count();


        public int _page = 0;
        public int Page { 
            get 
            {
                return Math.Clamp(_page, 0, MaxPage);
            }
            set
            {
                _page = Math.Clamp(value, 0, MaxPage);
            }
        }
        public int MaxPage => (int)(SearchResults / 10f);

        public IFilter? OpendFilter => Filters.FirstOrDefault(e => e.PopUp);

        protected override async Task OnInitializedAsync()
        {
            TimeEntries = new TimeEntry[TimeEntrySource.GetEntries().Count];
            TimeEntrySource.GetEntries().CopyTo(TimeEntries, 0);
            TimeEntries = TimeEntries.Reverse().ToArray();

            foreach (IFilter Filter in Filters)
            {
                Filter.FilterChanged += FilterHasChanged;
            }

            var Days = TimeEntries.GroupBy(e => e.Start.Date);

            foreach (var Day in Days)
            {
                if (!Day.First().Title.Equals("Krank"))
                {
                    var TimeLeft = NeededDailyTime;
                    foreach (var Entry in Day)
                    {
                        TimeLeft -= Entry.Time;
                    }
                    Overtime -= TimeLeft;
                }
            }
            
            RefreshTimer = new Timer(UpdateTimer, null, 0, 1000);
        }

        private void FilterHasChanged(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        private void OnFilterClick(IFilter Filter)
        {
            if(OpendFilter == null)
                Filter.PopUp = true;
        }

        private void StopFilter(IFilter Filter)
        {
            Filter.Enabled = false;
        }

        public bool DoFiltersApply(TimeEntry Entry)
        {
            return Filters[0].MatchesCriteria(Entry.Start) && Filters[1].MatchesCriteria(Entry.Start) &&
                   Filters[2].MatchesCriteria(Entry.End) && Filters[3].MatchesCriteria(Entry.Title) &&
                   Filters[4].MatchesCriteria(Entry.Description);
        }

        public void OnNextClick()
        {
            Page++;
        }

        public void OnPreviousClick()
        {
            Page--;
        }

        public void UpdateTimer(object? State)
        {
            InvokeAsync(StateHasChanged);
        }

        void IDisposable.Dispose()
        {
            RefreshTimer?.Dispose();
        }

    }
}