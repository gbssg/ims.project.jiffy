using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Data.Filter
{
    public class StringFilter(string name) : AbstractFilter(name)
    {
        private string _value = string.Empty;

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
