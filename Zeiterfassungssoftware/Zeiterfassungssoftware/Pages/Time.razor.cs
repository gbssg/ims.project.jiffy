using Microsoft.AspNetCore.Components;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Activities;
using Zeiterfassungssoftware.Data.Time;


namespace Zeiterfassungssoftware.Pages
{

	public partial class Time
	{
		public bool Started { get; set; }
		public TimeEntry CurrentEntry { get; set; }

		protected override async Task OnInitializedAsync()
		{
			DataProvider.LoadData();	
		}

		private void ToggleClock()
		{
			Started = !Started;
			if(Started)
			{
				CurrentEntry = new()
				{
					Id = TimeEntryProvider.TimeEntries.Count + 1,
					Start = DateTime.Now,
					Title = "Test",
					Description = "Test lorem ipsum",
				};
			} 
			else
			{
				CurrentEntry.End = DateTime.Now;
				CurrentEntry.Time = CurrentEntry.End - CurrentEntry.Start;
                TimeEntryProvider.TimeEntries.Add(CurrentEntry);
			}
		}


	}
	


}