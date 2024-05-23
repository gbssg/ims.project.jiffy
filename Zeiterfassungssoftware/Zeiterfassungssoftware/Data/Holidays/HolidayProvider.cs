using System.Net.Http.Json;

namespace Zeiterfassungssoftware.Data.Holidays
{
    public class HolidayProvider
    {
        public async Task<Holidays> LoadHolidaysAsync() 
        {
            HttpClient http = new HttpClient();
            Holidays Holidays = await http.GetFromJsonAsync<Holidays>("https://daten.stadt.sg.ch/api/explore/v2.1/catalog/datasets/schulferien-feiertage-stadt-stgallen/records?limit=100");

            Holidays.results = Holidays.results.Where(e => e.betreff.ToLower().Contains("ferien")).Where(e => e.GetEndAsDate() >= new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).ToList();

            return Holidays;
        }

        public static async Task<bool> IsHolyDay() 
        {
            Holidays Holidays = await new HolidayProvider().LoadHolidaysAsync();

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
