using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Zeiterfassungssoftware.Client.Data.Filter;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class Time : IDisposable
    {
		
		[Inject]
		private ITimeEntryProvider TimeEntrySource { get; init; }

		[Inject]
		private IActivityProvider ActivitySource { get; init; }


		public bool Started => CurrentEntry != null && CurrentEntry.End == null;
		public bool Disabled => !(Started || !((ActivityTitleSelect.Equals(NEW_ACTIVITY_TITLE) && ActivityTitle.Trim().Equals("")) || (ActivityDescriptionSelect.Equals(NEW_ACTIVITY_DESCRIPTION) && ActivityDescription.Trim().Equals(""))));
		private TimeSpan PassedTime => (DateTime.Now - CurrentEntry?.Start) ?? TimeSpan.Zero;

		public TimeEntry? CurrentEntry { get; set; }
		private Timer? Timer;

		private const string NEW_ACTIVITY_TITLE = "New Activity";
		private const string NEW_ACTIVITY_DESCRIPTION = "New Description";

		private string ActivityTitleSelect = NEW_ACTIVITY_TITLE;
		private string ActivityTitle = string.Empty;
		private string Title => ActivityTitleSelect.Equals(NEW_ACTIVITY_TITLE) ? ActivityTitle : ActivityTitleSelect;


		private string ActivityDescriptionSelect = NEW_ACTIVITY_DESCRIPTION;
		private string ActivityDescription = string.Empty;
		private string Description => ActivityDescriptionSelect.Equals(NEW_ACTIVITY_DESCRIPTION) ? ActivityDescription : ActivityDescriptionSelect;

		public string ActivityDescriptionTextAreaStyle => ActivityDescriptionSelect.Equals(NEW_ACTIVITY_DESCRIPTION) ? "" : "display: none;";
		public string ActivityTitleTextAreaStyle => ActivityTitleSelect.Equals(NEW_ACTIVITY_TITLE) ? "" : "display: none;";

		public bool Loaded = false;


		protected override void OnInitialized()
		{
			Timer = new Timer(UpdateTimer, null, 0, 1000);
		}

		public void Init()
		{
            CurrentEntry = TimeEntrySource.GetEntries().FirstOrDefault();

            if ((CurrentEntry != null) && (CurrentEntry.End != null))
                CurrentEntry = null;

            if (!Started)
				Timer?.Dispose();
		}

		private void ToggleClock()
		{
			if (Disabled)
				return;

			if (string.Equals(Title, "Krank"))
            {
				TimeEntry Entry = new()
				{
					Start = new DateTime(DateOnly.FromDateTime(DateTime.Now), new TimeOnly(0,0,0)),
					End = new DateTime(DateOnly.FromDateTime(DateTime.Now), new TimeOnly(23, 59, 59)),
					Title = "Krank",
					Description = this.Description,
				};

				TimeEntrySource.Add(Entry);
				
                ResetValues();
                return;
			}

			if (!Started)
			{
				if (ActivityTitleSelect.Equals(NEW_ACTIVITY_TITLE))
				{
					var Title = new ActivityTitle()
					{
						Value = ActivityTitle,
					};

					if (!ActivitySource.Contains(Title))
						ActivitySource.Add(Title);
				}


				if (ActivityDescriptionSelect.Equals(NEW_ACTIVITY_DESCRIPTION))
				{
                    var Description = new ActivityDescription()
                    {
                        Value = ActivityDescription,
                    };

                    if (!ActivitySource.Contains(Description))
						ActivitySource.Add(Description);
				}

				Timer = new System.Threading.Timer(UpdateTimer, null, 0, 1000);
				CurrentEntry = new()
				{
					Start = DateTime.Now,
					Title = this.Title,
					Description = this.Description,
				};

				TimeEntrySource.Add(CurrentEntry);
            }
			else if (CurrentEntry != null)
			{
				Timer?.Dispose();
				CurrentEntry.End = DateTime.Now;


				if (CurrentEntry.Time < new TimeSpan(0, 0, 1))
					TimeEntrySource.Remove(CurrentEntry);
				else
					TimeEntrySource.Update(CurrentEntry);

				ResetValues();
			}
			else
			{
				Timer?.Dispose();
				throw new Exception("CurrentEntry was null even though the measuring was not started!");
			}
		}

		public void ResetValues()
		{
            ActivityTitleSelect = NEW_ACTIVITY_TITLE;
            ActivityDescriptionSelect = NEW_ACTIVITY_DESCRIPTION;
            ActivityTitle = string.Empty;
            ActivityDescription = string.Empty;
        }

		public void UpdateTimer(object? obj)
		{
			if(TimeEntrySource.IsLoaded && ActivitySource.IsLoaded && !Loaded)
			{
				Loaded = true;
				Init();
			}

			InvokeAsync(StateHasChanged);
		}


		void IDisposable.Dispose()
		{
			Timer?.Dispose();
		}
	}
}