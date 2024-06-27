
namespace Zeiterfassungssoftware.Data.Filter
{
    public abstract class AbstractFilter(string name) : IFilter
    {
        public string Name { get; } = name;
        public bool Enabled { get; set; } = false;

        public event EventHandler? FilterChanged;
        public bool PopUp { get; set; }

        public abstract bool MatchesCriteria(object Input);

        protected void NotifySubscribers()
        {
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
