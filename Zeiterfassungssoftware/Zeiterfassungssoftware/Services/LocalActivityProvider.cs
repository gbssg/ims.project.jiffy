using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{
	public class LocalActivityProvider : IActivityProvider
	{
		private List<ActivityTitle> _activityTitles = [];
		private List<ActivityDescription> _activityDescriptions = [];

		public LocalActivityProvider() 
		{
			_activityTitles = new List<ActivityTitle>()
			{
				new ActivityTitle("Arbeit"),
				new ActivityTitle("Integrierte Praxisarbeit"),
				new ActivityTitle("Englisch"),
				new ActivityTitle("English BMS"),
				new ActivityTitle("M122"),
				new ActivityTitle("M431")
			};
			_activityDescriptions = new List<ActivityDescription>()
			{
				new ActivityDescription("Arbeit am Projekt Zeiterfassungssoftware"),
				new ActivityDescription("Projekt"),
				new ActivityDescription("Lernen für Prüfung in 2 Wochen"),
				new ActivityDescription("Programmieren"),
				new ActivityDescription("Prüfung"),
				new ActivityDescription("BugFix in LocalTimeEntryProvider.cs"),
                new ActivityDescription("Englisch Vokabular gelernt"),
                new ActivityDescription("Selbstständige arbeit an der Website"),
            };
		}

		public bool IsLoaded => true;

		public void Add(ActivityDescription ActivityDescription)
		{
			_activityDescriptions.Add(ActivityDescription);
		}

		public void Add(ActivityTitle ActivityTitle)
		{
			_activityTitles.Add(ActivityTitle);
		}

		public List<ActivityDescription> GetActivityDescriptions()
		{
			return _activityDescriptions;
		}

		public List<ActivityTitle> GetActivityTitles()
		{
			return _activityTitles;
		}

		public void Remove(ActivityDescription ActivityDescription)
		{
			_activityDescriptions.Remove(ActivityDescription);
		}

		public void Remove(ActivityTitle ActivityTitle)
		{
			_activityTitles.Remove(ActivityTitle);
		}

		public bool Contains(ActivityTitle ActivityTitle)
		{
			return _activityTitles.Contains(ActivityTitle);
		}

        public bool Contains(ActivityDescription ActivityDescription)
        {
            return _activityDescriptions.Contains(ActivityDescription);
        }
    }
}
