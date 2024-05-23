namespace Zeiterfassungssoftware.Data.Activities
{
    public class ActivityProvider
    {

        public List<ActivityTitle> LoadSavedTitles()
        {
            List<ActivityTitle> Titles = new List<ActivityTitle>()
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
            return Titles;
        }

        public List<ActivityDescription> LoadSavedDescriptions() 
        {
            List<ActivityDescription> Descriptions = new List<ActivityDescription>()
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
            return Descriptions;
        }
        
    }
}
