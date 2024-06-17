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

        public bool FilterColapsed { get; set; } = true;
        public string FilterCssClass => FilterColapsed ? "filter-dropdown-colapsed" : "filter-dropdown";

        private IFilter[] filters =
        {
            new DateFilter("Datum"),
            new StringFilter("Titel"),
            new TimeFilter("Time"),
        };

        public IFilter DateFilter => filters[0];
        public IFilter TitleFilter => filters[1];
        public IFilter TimeFilter => filters[2];

        public int Page { get; set; } = 0;


        protected override async Task OnInitializedAsync()
        {
            TimeEntries = new TimeEntry[TimeEntrySource.GetEntries().Count];
            TimeEntrySource.GetEntries().CopyTo(TimeEntries, 0);
            TimeEntries = TimeEntries.Reverse().ToArray();

            foreach (var f in filters)
            {
                f.Enabled = true;
                f.FilterChanged += FilterHasChanged;
            }

            var entries = TimeEntrySource.GetEntries();
            var days = entries.GroupBy(e => e.Start.Date);

            foreach (var day in days )
            {
                if (!day.First().Title.Equals("Krank"))
                {
                    var timeLeft = NeededDailyTime;
                    foreach (var entry in day)
                    {
                        timeLeft -= entry.Time;
                    }
                    Overtime -= timeLeft;
                }
            }
        }

        private void FilterHasChanged(object? sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        private void OnFilterClick()
        {
            FilterColapsed = !FilterColapsed;
        }

        public bool DoFiltersApply(TimeEntry Entry)
        {
            return TimeFilter.MatchesCriteria(Entry.Start) && TitleFilter.MatchesCriteria(Entry.Title) &&
                   DateFilter.MatchesCriteria(Entry.Start);
        }

        public void OnNextClick()
        {
            Page++;

            if(Page*10 > TimeEntrySource.GetEntries().Count)
                Page = (int)Math.Round(TimeEntrySource.GetEntries().Count / 10f);
        }

        public void OnPreviousClick()
        {
            Page--;

            if (Page < 0)
                Page = 0;
        }
        
    }
}