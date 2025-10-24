using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Times
{
    public interface ITimeEntryProvider
    {
        public bool IsLoaded { get; }

        public void Add(TimeEntry Entry);
		public Task Remove(TimeEntry Entry);
        public Task<TimeEntry> GetEntryById(Guid Id);
        public List<TimeEntry> GetEntries();
        public Task Update(TimeEntry Entry);

    }
}
