using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Zeiterfassungssoftware.SharedData.Times;
using System.Net;

namespace Zeiterfassungssoftware.Client.Services
{

	public class RemoteTimeEntryProvider : ITimeEntryProvider
	{
		public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        public HttpClient HttpClient { get; set; } = new HttpClient()
		{
			BaseAddress = new Uri("https://localhost:7099/api/v1/entries/")
		};

		private List<TimeEntryDto> _timeEntries = [];
		public bool IsLoaded { get; private set; }


		public RemoteTimeEntryProvider() 
		{
			LoadEntries();
		}

		public async void LoadEntries()
		{
			int i = 0;
			while(true)
			{
				var Entries = await HttpClient.GetFromJsonAsync<List<TimeEntryDto>>($"?start={30*i}&limit=30") ?? new();
				IsLoaded = true;

				if (!Entries.Any())
					break;

                _timeEntries.AddRange(Entries);
                IsLoaded = true;
                i++;
			}
		}

		public async Task<TimeEntryDto> CreateEntry(TimeEntryDto entry)
		{
			var Response = await HttpClient.PostAsJsonAsync("", entry);
			
			try
			{
				Response.EnsureSuccessStatusCode();
				var ReponseContent = await Response.Content.ReadAsStringAsync();
				var ConfirmedEntry = JsonSerializer.Deserialize<TimeEntryDto>(ReponseContent, Options) ?? new();
                _timeEntries.Add(ConfirmedEntry);
				return ConfirmedEntry;
			}
			catch (Exception e) 
			{
				throw new InvalidDataException();
			}
		}

		public List<TimeEntryDto> GetEntries()
		{
			return _timeEntries;
		}

		public async Task<TimeEntryDto?> GetEntryById(Guid id)
		{
			var TimeEntry = await HttpClient.GetFromJsonAsync<TimeEntryDto>(id.ToString());

			if (TimeEntry is null)
				throw new KeyNotFoundException();

            return TimeEntry;
		}


        public async Task DeleteEntry(Guid id)
		{
			var Response = await HttpClient.DeleteAsync(id.ToString());
			
			try
			{
                Response.EnsureSuccessStatusCode();
				var Entry = _timeEntries.FirstOrDefault(e => e.Id == id);

				if (Entry is not null)
					_timeEntries.Remove(Entry);
            } 
			catch(Exception e)
			{
				throw new KeyNotFoundException();
            }
		}

        public async Task<TimeEntryDto> UpdateEntry(Guid id, TimeEntryDto entry)
        {
			var Response = await HttpClient.PutAsJsonAsync(id.ToString(), entry);

			try
			{
				Response.EnsureSuccessStatusCode();

				var Body = await Response.Content.ReadAsStringAsync();
				var ConfirmedEntry = JsonSerializer.Deserialize<TimeEntryDto>(Body);

				if (ConfirmedEntry is null)
					throw new Exception();


				var Entry = _timeEntries.FirstOrDefault(e => e.Id == id);
				if(Entry is null)
				{
					_timeEntries.Add(ConfirmedEntry);
				}
				else
				{
					var index = _timeEntries.IndexOf(Entry);
					_timeEntries[index] = ConfirmedEntry;
				}

				return ConfirmedEntry;
            }
			catch (Exception e) {
				if (Response.StatusCode == HttpStatusCode.NotFound)
					throw new KeyNotFoundException();
				else
					throw new InvalidDataException();
			}
        }
    }
}