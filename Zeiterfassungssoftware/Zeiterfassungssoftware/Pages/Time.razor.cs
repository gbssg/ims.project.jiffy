using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Services;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;


namespace Zeiterfassungssoftware.Pages
{

    public partial class Time
	{
		public bool Started => CurrentEntry != null && CurrentEntry.End == null;
		private TimeSpan PassedTime => DateTime.Now - CurrentEntry.Start;

		public TimeEntry? CurrentEntry { get; set; }
		private Timer Timer;

		private readonly string NewActivityTitle = "Neue Tätigkeit erfassen";
		private readonly string NewActivityDescription = "Neue Beschreibung erfassen";

		private string ActivityTitleSelect = "Neue Tätigkeit erfassen";
        private string ActivityTitle = string.Empty;

		private string ActivityDescriptionSelect = "Neue Beschreibung erfassen";
		private string ActivityDescription = string.Empty;


        protected override async Task OnInitializedAsync()
		{
			CurrentEntry = TimeEntrySource.GetEntries().Last();
			if ((CurrentEntry != null) 
				&& (CurrentEntry.End != null))
			{
				CurrentEntry = null;
			}

			if (Started)
            {
				Timer = new System.Threading.Timer(UpdateTimer, null, 0, 1000);
            }
		}

		private void ToggleClock()
		{
			
			if (!Started)
			{
				if (ActivityTitleSelect.Equals(NewActivityTitle))
				{
                    if (ActivityTitle.Trim() == string.Empty)
                        return;

					var Title = new ActivityTitle(ActivityTitle);

                    if (!ActivitySource.Contains(Title))
						ActivitySource.Add(Title);
				}

				
                if (ActivityDescriptionSelect.Equals(NewActivityDescription))
				{
					if (ActivityDescription.Trim() == string.Empty)
						return;

                    var Description = new ActivityDescription(ActivityTitle);

					if(!ActivitySource.Contains(Description)) 
						ActivitySource.Add(Description);
				}
                
                Timer = new System.Threading.Timer(UpdateTimer, null, 0, 1000);
				CurrentEntry = new()
				{
					Start = DateTime.Now,
					Title = ActivityTitleSelect.Equals(NewActivityTitle) ? ActivityTitle : ActivityTitleSelect,
					Description = ActivityDescriptionSelect.Equals(NewActivityDescription) ? ActivityDescription : ActivityDescriptionSelect,
				};

				TimeEntrySource.Add(CurrentEntry);
			}
			else if (CurrentEntry != null)
            {
				Timer.Dispose();
				CurrentEntry.End = DateTime.Now;
			}
			else
			{
				Timer.Dispose();
				throw new Exception("CurrenEntry was null even though the measuring was not started!");
			}
		}

		public void UpdateTimer(object obj)
		{
			InvokeAsync(StateHasChanged);
		}
		

		void IDisposable.Dispose()
		{
			Timer?.Dispose();
		}
		

	}
	



}