namespace Zeiterfassungssoftware.Data.Holidays
{
    public class Holiday
    {
        public string betreff { get; set; }
        public string beginnt_am { get; set; }
        public string endet_am { get; set; }

        public DateOnly GetBeginAsDate()
        {
            return ToDate(beginnt_am);
        }

        public DateOnly GetEndAsDate()
        {
            return ToDate(endet_am);
        }

        private DateOnly ToDate(string Input)
        {
            int Year = Convert.ToInt32(Input.Split("-")[0]);
            int Month = Convert.ToInt32(Input.Split("-")[1]);
            int Day = Convert.ToInt32(Input.Split("-")[2]);

            return new DateOnly(Year, Month, Day);
        }
    }
}
