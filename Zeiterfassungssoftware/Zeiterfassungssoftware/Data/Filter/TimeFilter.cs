using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Data.Filter
{
    public class TimeFilter : AbstractFilter
    {
        public TimeOnly MinTime { get; set; }
        public TimeOnly MaxTime { get; set; }

        public TimeFilter(string name) : base(name)
        {
        }

        public override bool MatchesCriteria(object Input)
        {
            if (!(Input is DateTime))
                return false;

            DateTime Entry = (DateTime)Input;

            TimeOnly timeOnly = new TimeOnly(Entry.Hour, Entry.Minute, Entry.Second);
            return !Enabled || (timeOnly <= MaxTime && timeOnly >= MinTime);
        }
    }
}
