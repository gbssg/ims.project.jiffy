using Microsoft.AspNetCore.Components;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Services;
using Zeiterfassungssoftware.SharedData.Time;


namespace Zeiterfassungssoftware.Pages
{

    public partial class Time
	{
		public bool Started => CurrentEntry != null && CurrentEntry.End == null;
		public TimeEntry? CurrentEntry { get; set; }

		private TimeSpan passedTime => DateTime.Now - CurrentEntry.Start;
		private System.Threading.Timer timer;

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
				timer = new System.Threading.Timer(UpdateTimer, null, 0, 1000);
            }
		}

		private void ToggleClock()
		{
			
			if (!Started)
			{
				timer = new System.Threading.Timer(UpdateTimer, null, 0, 1000);
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
				timer.Dispose();
				CurrentEntry.End = DateTime.Now;
			}
			else
			{
				timer.Dispose();
				throw new Exception("CurrenEntry was null even though the measuring was not started!");
			}
		}

		public void UpdateTimer(object obj)
		{
			InvokeAsync(StateHasChanged);
		}
		

		void IDisposable.Dispose()
		{
			timer.Dispose();
			
		}
		bool checkIfArgumentIsTrue = false;
		void CreateNewTask(ChangeEventArgs args)
		{
			if(args.Value.ToString() == "Neue Tätigkeit erfassen")
			{
				checkIfArgumentIsTrue = true;
			}
			else{
				checkIfArgumentIsTrue= false;
			}

		}



	}
	



}