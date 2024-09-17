using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Client.Services
{
	public class RemoteActivityProvider : IActivityProvider
	{

		public HttpClient HttpClient { get; set; } = new HttpClient();

        private List<ActivityTitle> _activityTitles = [];
        private List<ActivityDescription> _activityDescriptions = [];
        public bool IsLoaded => true;

		public RemoteActivityProvider()
		{
			LoadData();
        }

		public async void LoadData()
		{
			_activityTitles = await HttpClient.GetFromJsonAsync<List<ActivityTitle>>("https://localhost:7099/api/v1/activities/titles/all") ?? new();

			_activityDescriptions = await HttpClient.GetFromJsonAsync<List<ActivityDescription>>("https://localhost:7099/api/v1/activities/descriptions/all") ?? new();

			
		}

		public async void Add(object Obj)
		{
			if (Obj is ActivityDescription Description)
			{
				string JsonData = JsonSerializer.Serialize(Description);
				var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await HttpClient.PostAsync("https://localhost:7099/api/v1/activities/descriptions/new", Content);

				try
				{
					response.EnsureSuccessStatusCode();
					_activityDescriptions.Add(Description);
				}
				catch (Exception e) { Console.WriteLine("Failed to Send Description"); }

				return;
			}

			if (Obj is ActivityTitle Title)
			{
				string JsonData = JsonSerializer.Serialize(Title);
				var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

				HttpResponseMessage response = await HttpClient.PostAsync("https://localhost:7099/api/v1/activities/titles/new", Content);

				try
				{
					response.EnsureSuccessStatusCode();
					_activityTitles.Add(Title);
				}
				catch (Exception e) { Console.WriteLine("Failed to Send Title"); }
			}
		}

		public bool Contains(object Obj)
		{
			if(Obj is ActivityTitle Title)
				return _activityTitles.Contains(Title);

            if (Obj is ActivityDescription Description)
                return _activityDescriptions.Contains(Description);

            return false;
		}

		public List<ActivityDescription> GetActivityDescriptions()
		{
			return _activityDescriptions;
		}

		public List<ActivityTitle> GetActivityTitles()
		{
			return _activityTitles;
		}

		public void Remove(object Obj)
		{
			// delete
		}
	}
}
