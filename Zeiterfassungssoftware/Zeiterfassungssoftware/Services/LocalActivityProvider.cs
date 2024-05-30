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
				new ActivityTitle()
				{
					Value = "Arbeit"
				},
				new ActivityTitle()
				{
					Value = "Integrierte Praxisarbeit"
				},
				new ActivityTitle()
				{
					Value = "Englisch"
				},
				new ActivityTitle()
				{
					Value = "English BMS"
				},
				new ActivityTitle()
				{
					Value = "M122"
				},
				new ActivityTitle()
				{
					Value = "M431"
				}
			};
			_activityDescriptions = new List<ActivityDescription>()
			{
				new ActivityDescription()
				{
					Value = "Arbeit"
				},
				new ActivityDescription()
				{
					Value = "Projekt"
				},
				new ActivityDescription()
				{
					Value = "Lernen"
				},
				new ActivityDescription()
				{
					Value = "Programmieren"
				},
				new ActivityDescription()
				{
					Value = "Prüfung"
				}
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

		public List<ActivityDescription> GetActivityDescription()
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
	}
}
