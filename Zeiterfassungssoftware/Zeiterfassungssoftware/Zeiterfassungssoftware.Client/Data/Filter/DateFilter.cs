using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Data.Filter
{
    public class DateFilter : AbstractFilter
    {
        private DateOnly _minDate = new DateOnly(2023, 8, 14);
        private DateOnly _maxDate = DateOnly.FromDateTime(DateTime.Now);
        public DateOnly MinDate
        {
            get 
            { 
                return _minDate;
            }
            set
            {
                _minDate = value;
                NotifySubscribers();
            }
        }

        public DateOnly MaxDate
        {
            get
            {
                return _maxDate;
            }
            set
            {
                _maxDate = value;
                NotifySubscribers();
            }
        }

        public DateFilter(string name) : base(name) {}

        public override bool MatchesCriteria(object Input)
        {
            if(Input is not DateTime Entry)
                return false;

            DateOnly EntryDate = DateOnly.FromDateTime(Entry);
            return !Enabled || (EntryDate <= MaxDate && EntryDate >= MinDate);
        }
    }
}
