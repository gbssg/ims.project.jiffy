using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Zeiterfassungssoftware.Client.Services;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{
	public class ServerTimeEntryProvider : ITimeEntryProvider
	{
		private List<TimeEntry> _timeEntries = [];
        private IActivityProvider ActivitySource = new ServerActivityProvider();

        public event EventHandler? OnLoaded;

        public ServerTimeEntryProvider()
		{
			
		}

		public bool IsLoaded => true;

		public void Add(TimeEntry Entry)
		{
			_timeEntries.Add(Entry);
		}

		public async Task Remove(TimeEntry Entry)
		{
			_timeEntries.Remove(Entry);
		}

		public List<TimeEntry> GetEntries() => _timeEntries;
		public async Task<TimeEntry> GetEntryById(Guid Id) => _timeEntries.Where(e => e.Id == Id).FirstOrDefault();
		

		public async Task<int> GetEntryIndexById(Guid Id)
		{
			var Entry = await GetEntryById(Id);

			if (Entry is null)
				return -1;

			return _timeEntries.IndexOf(Entry);
		}

		public async Task Update(TimeEntry Entry)
		{
			var ExisitingIndex = await GetEntryIndexById(Entry.Id);

			if (ExisitingIndex == -1)
				throw new Exception("No Entry found.");

			_timeEntries[ExisitingIndex] = Entry;		
		}

    }
}
