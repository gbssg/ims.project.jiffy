using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Components.Data.Filter
{
    public interface IFilter
    {
        public string Name { get; }
        public bool Enabled { get; set; }

        public event EventHandler? FilterChanged;
        public bool PopUp { get; set; }

        public bool MatchesCriteria(object Input);
    }
}
