using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Data.Filter
{
    public class DateFilter : AbstractFilter
    {
        private DateOnly _minDate;
        public DateOnly MinDate
        {
            get { return _minDate; }
            set
            {
                _minDate = value;
                NotifySubscribers();
            }
        }

        private DateOnly _maxDate;

        public DateFilter(string name) : base(name)
        {
        }

        public DateOnly MaxDate
        {
            get { return _maxDate; }
            set
            {
                _maxDate = value;
                NotifySubscribers();
            }
        }

        public override bool MatchesCriteria(object Input)
        {
            if(Input is not DateTime)
                return false;

            DateTime Entry = (DateTime)Input;

            DateOnly EntryDate = new DateOnly(Entry.Year, Entry.Month, Entry.Day);
            return !Enabled || (EntryDate <= MaxDate && MinDate >= EntryDate);
        }
    }
}
