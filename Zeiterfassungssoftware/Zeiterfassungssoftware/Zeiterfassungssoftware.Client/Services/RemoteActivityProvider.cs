using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Client.Services
{
	public class RemoteActivityProvider : IActivityProvider
	{

        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };


        public HttpClient HttpClient { get; set; } = new HttpClient()
		{
			BaseAddress = new Uri("https://localhost:7099/api/v1/activities/")
		};
		
        private List<ActivityTitleDto> _activityTitles = [];
        private List<ActivityDescriptionDto> _activityDescriptions = [];
		public bool IsLoaded { get; private set; }

		public RemoteActivityProvider()
		{
			LoadData();
        }

		public async void LoadData()
		{
			_activityTitles = await HttpClient.GetFromJsonAsync<List<ActivityTitleDto>>("titles") ?? new();
			_activityDescriptions = await HttpClient.GetFromJsonAsync<List<ActivityDescriptionDto>>("descriptions") ?? new();

			IsLoaded = true;
		}

		public async void Add(object Obj)
		{

			string JsonData = JsonSerializer.Serialize(Obj);
			var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

			if (Obj is ActivityDescriptionDto Description)
			{
				HttpResponseMessage Response = await HttpClient.PostAsync("descriptions", Content);

				try
				{
					Response.EnsureSuccessStatusCode();
					_activityDescriptions.Add(Description);
				}
				catch (Exception e) { Console.WriteLine($"Failed to Send Description: {e.Message}"); }

				return;
			}

			if (Obj is ActivityTitleDto Title)
			{
				
				HttpResponseMessage Response = await HttpClient.PostAsync("titles", Content);

				try
				{
					Response.EnsureSuccessStatusCode();
					_activityTitles.Add(Title);
				}
				catch (Exception e) { Console.WriteLine($"Failed to Send Title: {e.Message}"); }
			}
		}

        public async Task<object> Update(object Obj)
        {

            string JsonData = JsonSerializer.Serialize(Obj);
            var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

            if (Obj is ActivityDescriptionDto Description)
            {
                HttpResponseMessage Response = await HttpClient.PatchAsync($"descriptions/{Description.Id}", Content);

                try
                {
                    Response.EnsureSuccessStatusCode();
                    string ReponseContent = await Response.Content.ReadAsStringAsync();
                    ActivityDescriptionDto ResponseDescription = JsonSerializer.Deserialize<ActivityDescriptionDto>(ReponseContent, Options) ?? new();
                    return ResponseDescription;
                }
                catch (Exception e) { Console.WriteLine($"Failed to Send Description: {e.Message}"); }

            }

            if (Obj is ActivityTitleDto Title)
            {

                HttpResponseMessage Response = await HttpClient.PatchAsync($"titles/{Title.Id}", Content);

                try
                {
                    Response.EnsureSuccessStatusCode();
                    string ReponseContent = await Response.Content.ReadAsStringAsync();
                    ActivityTitleDto ResponseTitle = JsonSerializer.Deserialize<ActivityTitleDto>(ReponseContent, Options) ?? new();
					return ResponseTitle;
				}
                catch (Exception e) { Console.WriteLine($"Failed to Send Title: {e.Message}"); }
            }
            return null;
        }

        public bool Contains(object Obj)
		{
			if(Obj is ActivityTitleDto Title)
				return _activityTitles.Contains(Title);

            if (Obj is ActivityDescriptionDto Description)
                return _activityDescriptions.Contains(Description);

            return false;
		}

		public List<ActivityDescriptionDto> GetActivityDescriptions()
		{
			return _activityDescriptions;
		}

		public List<ActivityTitleDto> GetActivityTitles()
		{
			return _activityTitles;
		}

		public async void Remove(object Obj)
		{
            if (Obj is ActivityDescriptionDto Description)
            {
                HttpResponseMessage Response = await HttpClient.DeleteAsync($"descriptions/{Description.Id}");

                try
                {
                    Response.EnsureSuccessStatusCode();
                    _activityDescriptions.Remove(Description);
                }
                catch (Exception e) { Console.WriteLine($"Failed to Send Description: {e.Message}"); }

            }

            if (Obj is ActivityTitleDto Title)
            {

                HttpResponseMessage Response = await HttpClient.DeleteAsync($"titles/{Title.Id}");

                try
                {
                    Response.EnsureSuccessStatusCode();
					_activityTitles.Remove(Title);
                }
                catch (Exception e) { Console.WriteLine($"Failed to Send Title: {e.Message}"); }
            }
        }
	}
}
