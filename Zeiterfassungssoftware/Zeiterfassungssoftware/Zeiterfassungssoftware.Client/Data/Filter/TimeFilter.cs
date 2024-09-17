using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Data.Filter
{
    public class TimeFilter(string name) : AbstractFilter(name)
    {

        private TimeOnly _minTime = TimeOnly.MinValue;
        private TimeOnly _maxTime = TimeOnly.MaxValue;

        public TimeOnly MinTime { 
            get 
            {
                return _minTime;
            }
            set 
            {
                _minTime = value;
                NotifySubscribers();
            }
        }
        public TimeOnly MaxTime
        {
            get
            {
                return _maxTime;
            }
            set
            {
                _maxTime = value;
                NotifySubscribers();
            }
        }

        public override bool MatchesCriteria(object Input)
        {
            if (Input is not DateTime Entry)
                return false;

            TimeOnly timeOnly = TimeOnly.FromDateTime(Entry);
            return !Enabled || (timeOnly <= MaxTime && timeOnly >= MinTime);
        }
    }
}
