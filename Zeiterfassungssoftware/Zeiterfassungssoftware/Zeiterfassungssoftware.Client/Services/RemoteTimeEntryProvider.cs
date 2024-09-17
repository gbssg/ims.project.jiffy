using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
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

		public async void Add(TimeEntry Entry)
		{

			string JsonData = JsonSerializer.Serialize(Entry);
			var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

			HttpResponseMessage Response = await HttpClient.PostAsync("https://localhost:7099/api/v1/entries/new", Content);

			try
			{
				Response.EnsureSuccessStatusCode();
				_timeEntries.Add(Entry);
			}
			catch (Exception e) { Console.WriteLine("Failed to Send Entry"); }

			return;
			
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