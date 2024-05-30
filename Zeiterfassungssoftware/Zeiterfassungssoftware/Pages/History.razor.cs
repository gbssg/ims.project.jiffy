using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Services;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Pages
{
    public partial class History
    {
        //test
        public TimeEntry[] TimeEntries { get; set; }

        public int SickDays => TimeEntrySource.GetEntries().Where(e => e.Title.ToLower().Trim().Equals("krank")).Count();
        public TimeSpan Overtime = new TimeSpan(0, 0, 0);

        public TimeSpan NeededDailyTime = new TimeSpan(6, 30, 0);

        public bool FilterColapsed { get; set; } = true;
        public string FilterCssClass => FilterColapsed ? "filter-dropdown-colapsed" : "filter-dropdown";

        

		protected override async Task OnInitializedAsync()
        {
            TimeEntries = new TimeEntry[TimeEntrySource.GetEntries().Count];
            TimeEntrySource.GetEntries().CopyTo(TimeEntries, 0);
            TimeEntries = TimeEntries.Reverse().ToArray();

            foreach (var Entry in TimeEntrySource.GetEntries())
            {
                Overtime += Entry.Time.Subtract(NeededDailyTime);
            }
        }

        private void OnFilterClick()
        {
            FilterColapsed = !FilterColapsed;
        }
    }
}