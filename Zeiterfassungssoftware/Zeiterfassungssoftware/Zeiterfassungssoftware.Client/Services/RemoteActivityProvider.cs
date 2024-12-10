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

		public HttpClient HttpClient { get; set; } = new HttpClient()
		{
			BaseAddress = new Uri("https://localhost:7099/api/v1/activities/")
		};
		
        private List<ActivityTitle> _activityTitles = [];
        private List<ActivityDescription> _activityDescriptions = [];
		public bool IsLoaded { get; private set; }

		public RemoteActivityProvider()
		{
			LoadData();
        }

		public async void LoadData()
		{
			_activityTitles = await HttpClient.GetFromJsonAsync<List<ActivityTitle>>("titles/all") ?? new();
			_activityDescriptions = await HttpClient.GetFromJsonAsync<List<ActivityDescription>>("descriptions/all") ?? new();

			IsLoaded = true;
		}

		public async void Add(object Obj)
		{

			string JsonData = JsonSerializer.Serialize(Obj);
			var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

			if (Obj is ActivityDescription Description)
			{
				HttpResponseMessage Response = await HttpClient.PostAsync("descriptions/new", Content);

				try
				{
					Response.EnsureSuccessStatusCode();
					_activityDescriptions.Add(Description);
				}
				catch (Exception e) { Console.WriteLine("Failed to Send Description"); }

				return;
			}

			if (Obj is ActivityTitle Title)
			{
				
				HttpResponseMessage Response = await HttpClient.PostAsync("titles/new", Content);

				try
				{
					Response.EnsureSuccessStatusCode();
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
