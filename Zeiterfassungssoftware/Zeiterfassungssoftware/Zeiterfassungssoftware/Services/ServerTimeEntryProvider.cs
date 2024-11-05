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
			int id = 0;

			for (int day = 1; day <= 13; day++)
			{
				if (day % new Random().Next(1, 5) != 0)
				{
					for (int i = 0; i < 8; i++)
					{
						TimeEntry Entry = new TimeEntry()
						{
							Start = new DateTime(2024, 6, day, 8 + i, 20, 0),
							End = new DateTime(2024, 6, day, 8 + i + 1, 20, 0),
							Title = ActivitySource.GetActivityTitles()[new Random().Next(0, ActivitySource.GetActivityTitles().Count)].Value,
							Description = ActivitySource.GetActivityDescriptions()[new Random().Next(0, ActivitySource.GetActivityDescriptions().Count)].Value
						};

						_timeEntries.Add(Entry);
						id++;
					}

				}
				else
				{
					TimeEntry Entry = new TimeEntry()
					{
						Start = new DateTime(2024, 6, day, 0, 0, 0),
						End = new DateTime(2024, 6, day, 23, 59, 59),
						Title = "Krank",
						Description = ""
					};

					_timeEntries.Add(Entry);
					id++;
				}

			}

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
