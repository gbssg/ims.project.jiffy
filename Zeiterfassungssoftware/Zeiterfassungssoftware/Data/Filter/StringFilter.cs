using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Data.Filter
{
    public class StringFilter : AbstractFilter
    {
        private string _value = string.Empty;

        public StringFilter(string name) : base(name) {}

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                NotifySubscribers();
            }
        }

        public override bool MatchesCriteria(object Input)
        {

            if (Input is not string Entry)
            {
                return false;
            }

            var valueNormalized = Normalize(Value);
            var entryNormalized = Normalize(Entry);

            return !Enabled || entryNormalized.Contains(valueNormalized);
        }

        private static string Normalize(string val)
        {
            return val.ToLower()
                      .Trim();
        }
    }
}
