using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Time
{
    public interface ITimeEntryProvider
    {
        public bool IsLoaded { get; }

        public void Add(TimeEntry entry);
        public List<TimeEntry> GetEntries();
    }
}
