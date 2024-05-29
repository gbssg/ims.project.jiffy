namespace Zeiterfassungssoftware.Data.Activities
{
    public class ActivityProvider
    {
        public static List<ActivityTitle> ActivityTitles { get; set; }
        public static List<ActivityDescription> ActivityDescriptions { get; set; }


        public static void LoadActivityTitles()
        {
            ActivityTitles = new List<ActivityTitle>()
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
        }

        public static void LoadActivityDescriptions() 
        {
            ActivityDescriptions = new List<ActivityDescription>()
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
        
    }
}
