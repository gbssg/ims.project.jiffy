
namespace Zeiterfassungssoftware.Data.Holidays
{
    public class Holidays
    {
        public int total_count { get; set; }
        public List<Holiday> results { get; set; }

        public static implicit operator Holidays?(Holiday? v)
        {
            throw new NotImplementedException();
        }
    }
}
