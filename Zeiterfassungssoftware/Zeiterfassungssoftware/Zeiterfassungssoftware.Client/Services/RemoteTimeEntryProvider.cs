using System.Net.Http;
using System.Net.Http.Json;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Services
{

	public class RemoteTimeEntryProvider : ITimeEntryProvider
	{

        public HttpClient HttpClient { get; set; } = new HttpClient();

		private List<TimeEntry> _timeEntries = [];
		public bool IsLoaded => true;


		public RemoteTimeEntryProvider() 
		{
			LoadEntries();
		}

		public async void LoadEntries()
		{
			_timeEntries = await HttpClient.GetFromJsonAsync<List<TimeEntry>>("https://localhost:7099/api/v1/entries/all") ?? new();
		}

		public void Add(TimeEntry Entry)
		{


		}

		public List<TimeEntry> GetEntries()
		{
			return _timeEntries;
		}

		public void Remove(TimeEntry Entry)
		{
			
		}
	}
}