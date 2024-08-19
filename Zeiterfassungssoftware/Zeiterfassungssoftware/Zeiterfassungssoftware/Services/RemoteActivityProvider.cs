using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{
    public class RemoteActivityProvider : IActivityProvider
    {
        private readonly List<ActivityTitle> _activityTitles = [];
        private readonly List<ActivityDescription> _activityDescriptions = [];

        public RemoteActivityProvider()
        {
            _activityTitles =
            [
                new ("Krank"),
                new ("Arbeit"),
                new ("Integrierte Praxisarbeit"),
                new ("Englisch"),
                new ("English BMS"),
                new ("M122"),
                new ("M431")
            ];
            _activityDescriptions =
            [
                new ("Arbeit am Projekt Zeiterfassungssoftware"),
                new ("Projekt"),
                new ("Lernen für Prüfung in 2 Wochen"),
                new ("Programmieren"),
                new ("Prüfung"),
                new ("BugFix in LocalTimeEntryProvider.cs"),
                new ("Englisch Vokabular gelernt"),
                new ("Selbstständige arbeit an der Website"),
            ];
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
