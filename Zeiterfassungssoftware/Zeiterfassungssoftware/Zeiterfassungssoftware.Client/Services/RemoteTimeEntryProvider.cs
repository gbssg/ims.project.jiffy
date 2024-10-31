using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;
using System.Text.Json.Serialization;

namespace Zeiterfassungssoftware.Client.Services
{

	public class RemoteTimeEntryProvider : ITimeEntryProvider
	{

        public HttpClient HttpClient { get; set; } = new HttpClient()
		{
			BaseAddress = new Uri("https://localhost:7099/api/v1/entries/")
		};

		private List<TimeEntry> _timeEntries = [];
		public bool IsLoaded { get; private set; }


		public RemoteTimeEntryProvider() 
		{
			LoadEntries();
			
		}

		public async void LoadEntries()
		{
			_timeEntries = await HttpClient.GetFromJsonAsync<List<TimeEntry>>("all") ?? new();

			IsLoaded = true;
		}

		public async void Add(TimeEntry Entry)
		{

			string JsonData = JsonSerializer.Serialize(Entry);
			var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

			HttpResponseMessage Response = await HttpClient.PostAsync("new", Content);

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

		public async Task<TimeEntry> GetEntryById(Guid Id)
		{
			return await HttpClient.GetFromJsonAsync<TimeEntry>($"id/{Id}");
		}

		public async Task Remove(TimeEntry Entry)
		{
			try
			{
				HttpResponseMessage Message = await HttpClient.DeleteAsync($"delete/{Entry.Id}");
				Message.EnsureSuccessStatusCode();
				_timeEntries.Remove(Entry);
			} catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

    }
}