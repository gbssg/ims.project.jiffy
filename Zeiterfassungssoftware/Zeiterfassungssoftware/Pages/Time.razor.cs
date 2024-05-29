using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Services;
using Zeiterfassungssoftware.SharedData.Time;


namespace Zeiterfassungssoftware.Pages
{

    public partial class Time
	{
		public bool Started => CurrentEntry != null && CurrentEntry.End == null;
		public TimeEntry? CurrentEntry { get; set; }

		protected override async Task OnInitializedAsync()
		{
			CurrentEntry = TimeEntrySource.GetEntries().Last();
			if ((CurrentEntry != null) 
				&& (CurrentEntry.End != null))
			{
				CurrentEntry = null;
			}
		}

		private void ToggleClock()
		{
			if (!Started)
			{
				CurrentEntry = new()
				{
					Start = DateTime.Now,
					Title = "Test",
					Description = "Test lorem ipsum",
				};

				TimeEntrySource.Add(CurrentEntry);
			}
			else if (CurrentEntry != null)
            {
				CurrentEntry.End = DateTime.Now;
			}
			else
			{
				throw new Exception("CurrenEntry was null even though the measuring was not started!");
			}
		}


	}
	


}