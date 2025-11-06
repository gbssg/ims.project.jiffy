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

        public void Add(TimeEntryDto Entry);
		public Task Remove(TimeEntryDto Entry);
        public Task<TimeEntryDto> GetEntryById(Guid Id);
        public List<TimeEntryDto> GetEntries();
        public Task Update(TimeEntryDto Entry);

    }
}
