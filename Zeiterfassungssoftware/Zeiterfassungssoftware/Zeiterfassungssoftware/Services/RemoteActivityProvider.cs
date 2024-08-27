using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{
    public class RemoteActivityProvider : IActivityProvider
    {
        private readonly List<ActivityTitle> _activityTitles = [];
        private readonly List<ActivityDescription> _activityDescriptions = [];

        public bool IsLoaded => true;

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

        public void Add(object Obj)
        {
            if (Obj is ActivityTitle Title)
                _activityTitles.Add(Title);

            if (Obj is ActivityDescription Description)
                _activityDescriptions.Add(Description);
        }
        public void Remove(object Obj)
        {
            if (Obj is ActivityTitle Title)
                _activityTitles.Remove(Title);

            if (Obj is ActivityDescription Description)
                _activityDescriptions.Remove(Description);
        }

        public bool Contains(object Obj)
        {
            if(Obj is ActivityTitle Title)
                return _activityTitles.Contains(Title);

            if (Obj is ActivityDescription Description)
                return _activityDescriptions.Contains(Description);

            return false;
        }

        public List<ActivityDescription> GetActivityDescriptions() => _activityDescriptions;
        public List<ActivityTitle> GetActivityTitles() => _activityTitles;

    }
}
