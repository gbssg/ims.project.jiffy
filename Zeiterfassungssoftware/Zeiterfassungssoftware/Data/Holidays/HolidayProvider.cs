using System.Net.Http.Json;

namespace Zeiterfassungssoftware.Data.Holidays
{
    public class HolidayProvider
    {
        public static Holidays Holidays { get; set; }

        public static async void LoadHolidaysAsync()
        {
            HttpClient http = new HttpClient();
            Holidays ReadHolidays = await http.GetFromJsonAsync<Holidays>("https://daten.stadt.sg.ch/api/explore/v2.1/catalog/datasets/schulferien-feiertage-stadt-stgallen/records?limit=100");

            ReadHolidays.results = ReadHolidays.results.Where(e => e.betreff.ToLower().Contains("ferien")).Where(e => e.GetEndAsDate() >= new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).ToList();

            Holidays = ReadHolidays;
        }

        public static async Task<bool> IsHolyDay()
        {
            if(Holidays.results == null || Holidays.results.Count == 0) 
                return false;
            
            DateOnly Now = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            foreach (Holiday Holiday in Holidays.results)
            {
                if ((Now > Holiday.GetBeginAsDate()) && (Holiday.GetEndAsDate() > Now))
                {
                    return true;
                }

            }
            return false;
        }
    }
}
