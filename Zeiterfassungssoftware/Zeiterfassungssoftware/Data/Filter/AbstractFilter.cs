
namespace Zeiterfassungssoftware.Data.Filter
{
    public abstract class AbstractFilter : IFilter
    {
        public string Name { get; }
        public bool Enabled { get; set; } = false;

        public event EventHandler? FilterChanged;

        public AbstractFilter(string name)
        {
            Name = name;
        }

        public abstract bool MatchesCriteria(object Input);

        protected void NotifySubscribers()
        {
            FilterChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
