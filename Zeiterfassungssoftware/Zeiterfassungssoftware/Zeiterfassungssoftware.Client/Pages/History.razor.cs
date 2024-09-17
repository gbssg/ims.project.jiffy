using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Client.Data.Filter;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class History : IDisposable
	{
		[Inject]
		private ITimeEntryProvider TimeEntrySource { get; init; }

		public TimeEntry[] TimeEntries { get; set; }

		public int SickDays => TimeEntrySource.GetEntries().Where(e => e.Title.ToLower().Trim().Equals("krank")).Count();
		public TimeSpan Overtime = new(0, 0, 0);
		public TimeSpan NeededDailyTime = new(6, 30, 0);

		private readonly IFilter[] Filters =
		[
			new DateFilter("Date"),
			new TimeFilter("Start Time"),
			new TimeFilter("Stop Time"),
			new StringFilter("Activity"),
			new StringFilter("Description"),
		];

		public Timer? RefreshTimer;
		public int SearchResults => TimeEntries.Where(e => DoFiltersApply(e)).Count();
		public bool ShowFilters = true;

		public IFilter? OpendFilter => Filters.FirstOrDefault(e => e.PopUp);

		protected override void OnInitialized()
		{
			RefreshTimer = new Timer(UpdateTimer, null, 0, 1000);
		}

		public void Init()
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
                if (!Day.First().Title.ToLower().Equals("krank"))
                {
                    var TimeLeft = NeededDailyTime;
                    var WeekEndOvertime = TimeSpan.FromSeconds(0);

                    foreach (var Entry in Day)
                    {
                        if (Entry.IsWeekend)
                        {
                            Overtime += Entry.Time;
                            TimeLeft = TimeSpan.FromSeconds(0);
                        }
                        else
                        {
                            TimeLeft -= Entry.Time;
                        }
                    }
                    Overtime -= TimeLeft;
                }
            }
        }

		private void FilterHasChanged(object? sender, EventArgs e)
		{
			Refresh();
		}

		private void OnFilterClick(IFilter Filter)
		{
			if (OpendFilter == null)
				Filter.PopUp = true;

		}

		private void StopFilter(IFilter Filter)
		{
			Filter.Enabled = false;
		}

		public bool DoFiltersApply(TimeEntry Entry)
		{
			return Filters[0].MatchesCriteria(Entry.Start) && Filters[1].MatchesCriteria(Entry.Start) &&
							Filters[2].MatchesCriteria(Entry.End ?? DateTime.Now) && Filters[3].MatchesCriteria(Entry.Title) &&
							Filters[4].MatchesCriteria(Entry.Description);
		}

		public void UpdateTimer(object? State)
		{
			Refresh();

			if(TimeEntrySource.GetEntries().Count > 0 && TimeEntries == null)
			{
				Init();
			}
		}

		void IDisposable.Dispose()
		{
			RefreshTimer?.Dispose();
		}


		public void Refresh()
		{
			InvokeAsync(StateHasChanged);
		}
	}
}
