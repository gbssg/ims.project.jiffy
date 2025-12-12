using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class Time : IDisposable
    {

        private const string NEW_ACTIVITY_TITLE = "New Activity";
        private const string NEW_ACTIVITY_DESCRIPTION = "New Description";

        [Inject]
		private ITimeEntryProvider TimeEntrySource { get; init; }

		[Inject]
		private IActivityProvider ActivitySource { get; init; }


		public bool Started => CurrentEntry is not null 
							   && CurrentEntry.End is null;
		public bool Disabled => !Started && (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description));
		
		
		public TimeEntryDto? CurrentEntry { get; set; }

		private TimeSpan PassedTime => (DateTime.Now - CurrentEntry?.Start) ?? TimeSpan.Zero;
		private Timer? Timer;


		private string ActivityTitleSelect = NEW_ACTIVITY_TITLE;
		private string ActivityTitle = string.Empty;
		private string Title => string.Equals(ActivityTitleSelect, NEW_ACTIVITY_TITLE) ? ActivityTitle : ActivityTitleSelect;


		private string ActivityDescriptionSelect = NEW_ACTIVITY_DESCRIPTION;
		private string ActivityDescription = string.Empty;
		private string Description => string.Equals(ActivityDescriptionSelect, NEW_ACTIVITY_DESCRIPTION) ? ActivityDescription : ActivityDescriptionSelect;


		public bool Loaded = false;


		protected override void OnInitialized()
		{
			Timer = new Timer(UpdateTimer, null, 0, 1000);
		}

		public void Initialize()
		{
            CurrentEntry = TimeEntrySource.GetEntries().FirstOrDefault(e => e.End is null);

            if (!Started)
				Timer?.Dispose();
		}

		public async void ToggleClock()
		{
			if (Disabled)
				return;

			if (!Started)
			{
				PostTitle();
				PostDescription();

				Timer = new Timer(UpdateTimer, null, 0, 1000);

				var Temp = new TimeEntryDto()
				{
					Start = DateTime.Now,
					Title = this.Title,
					Description = this.Description,
				};

				if (!TimeEntrySource.PreValidateEntry(Temp))
					return;

				CurrentEntry = Temp;
                CurrentEntry = await TimeEntrySource.CreateEntry(Temp);
            }
			else
			{  
				if (CurrentEntry is null)
					return;

				CurrentEntry.End = DateTime.Now;
				await TimeEntrySource.UpdateEntry(CurrentEntry.Id, CurrentEntry);
				
				Timer?.Dispose();
				ResetValues();
			}
		}



		public async void PostTitle()
		{
            if (string.Equals(ActivityTitleSelect, NEW_ACTIVITY_TITLE))
            {
				if (string.Equals(ActivityTitle, NEW_ACTIVITY_TITLE))
					return;

                var Title = new ActivityTitleDto()
                {
                    Value = ActivityTitle,
                };

                if (!ActivitySource.GetTitles().Any(e => e.Value == ActivityTitle))
                    await ActivitySource.CreateTitle(Title);
            }
        }

        public async void PostDescription()
        {
            if (string.Equals(ActivityDescriptionSelect, NEW_ACTIVITY_DESCRIPTION))
            {
                if (string.Equals(ActivityDescription, NEW_ACTIVITY_DESCRIPTION))
                    return;

                var Description = new ActivityDescriptionDto()
                {
                    Value = ActivityDescription,
                };

                if (!ActivitySource.GetDescriptions().Any(e => e.Value == ActivityDescription))
                    await ActivitySource.CreateDescription(Description);
            }
        }

        public void ResetValues()
		{
            ActivityTitleSelect = NEW_ACTIVITY_TITLE;
            ActivityDescriptionSelect = NEW_ACTIVITY_DESCRIPTION;
            ActivityTitle = string.Empty;
            ActivityDescription = string.Empty;
			InvokeAsync(StateHasChanged);
        }

		public void UpdateTimer(object? obj)
		{
			if(TimeEntrySource.IsLoaded && ActivitySource.IsLoaded && !Loaded)
			{
				Loaded = true;
				Initialize();
			}

			InvokeAsync(StateHasChanged);
		}


		void IDisposable.Dispose()
		{
			Timer?.Dispose();
		}
	}
}