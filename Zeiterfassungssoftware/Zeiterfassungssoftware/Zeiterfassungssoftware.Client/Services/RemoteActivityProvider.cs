using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Zeiterfassungssoftware.Client.Pages;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Classes;

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
            LoadActivities();
        }

		public async void LoadActivities()
		{
			_activityTitles = await HttpClient.GetFromJsonAsync<List<ActivityTitleDto>>("titles") ?? new();
			_activityDescriptions = await HttpClient.GetFromJsonAsync<List<ActivityDescriptionDto>>("descriptions") ?? new();
            IsLoaded = true;
		}

        public async Task DeleteDescription(Guid id)
        {
            var Response = await HttpClient.DeleteAsync("descriptions/" + id.ToString());

            try
            {
                Response.EnsureSuccessStatusCode();
                var Description = _activityDescriptions.FirstOrDefault(e => e.Id == id);

                if (Description is not null)
                    _activityDescriptions.Remove(Description);
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ActivityDescriptionDto> CreateDescription(ActivityDescriptionDto activityDescription)
        {
            var Response = await HttpClient.PostAsJsonAsync("descriptions", activityDescription);

            try
            {
                Response.EnsureSuccessStatusCode();
                var ReponseContent = await Response.Content.ReadAsStringAsync();
                var ConfirmedDescription = JsonSerializer.Deserialize<ActivityDescriptionDto>(ReponseContent, Options) ?? new();

                _activityDescriptions.Add(ConfirmedDescription);
                return ConfirmedDescription;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }

        public async Task<ActivityDescriptionDto> UpdateDescription(Guid id, ActivityDescriptionDto activityDescription)
        {
            var Response = await HttpClient.PutAsJsonAsync($"descriptions/{id}",activityDescription);

            try
            {
                Response.EnsureSuccessStatusCode();

                var Body = await Response.Content.ReadAsStringAsync();
                var ConfirmedDescription = JsonSerializer.Deserialize<ActivityDescriptionDto>(Body, Options);

                if (ConfirmedDescription is null)
                    throw new Exception();

                var Description = _activityDescriptions.FirstOrDefault(e => e.Id == id);
                if (Description is null)
                {
                    _activityDescriptions.Add(ConfirmedDescription);
                }
                else
                {
                    var index = _activityDescriptions.IndexOf(Description);
                    _activityDescriptions[index] = ConfirmedDescription;
                }

                return ConfirmedDescription;
            }
            catch (Exception e)
            {
                if (Response.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();
                else
                    throw new InvalidDataException();
            }
        }

        public async Task<ActivityDescriptionDto> GetDescriptionById(Guid id)
        {
            var Description = await HttpClient.GetFromJsonAsync<ActivityDescriptionDto>("titles/" + id.ToString());

            if (Description is null)
                throw new KeyNotFoundException();

            return Description;
        }

        public List<ActivityDescriptionDto> GetDescriptions()
        {
            return _activityDescriptions;
        }

        public async Task DeleteTitle(Guid id)
        {
            var Response = await HttpClient.DeleteAsync("titles/"+id.ToString());

            try
            {
                Response.EnsureSuccessStatusCode();
                var Title = _activityTitles.FirstOrDefault(e => e.Id == id);

                if (Title is not null)
                    _activityTitles.Remove(Title);
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ActivityTitleDto> CreateTitle(ActivityTitleDto activityTitle)
        {
            var Response = await HttpClient.PostAsJsonAsync("titles", activityTitle);

            try
            {
                Response.EnsureSuccessStatusCode();
                var ReponseContent = await Response.Content.ReadAsStringAsync();
                var ConfirmedTitle = JsonSerializer.Deserialize<ActivityTitleDto>(ReponseContent, Options) ?? new();

                _activityTitles.Add(ConfirmedTitle);
                return ConfirmedTitle;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }

        public async Task<ActivityTitleDto> UpdateTitle(Guid id, ActivityTitleDto activityTitle)
        {
            var Response = await HttpClient.PutAsJsonAsync($"titles/{id}", activityTitle);

            try
            {
                Response.EnsureSuccessStatusCode();

                var Body = await Response.Content.ReadAsStringAsync();
                var ConfirmedTitle = JsonSerializer.Deserialize<ActivityTitleDto>(Body, Options);

                if (ConfirmedTitle is null)
                    throw new Exception();

                var Title = _activityTitles.FirstOrDefault(e => e.Id == id);
                if (Title is null)
                {
                    _activityTitles.Add(ConfirmedTitle);
                }
                else
                {
                    var index = _activityTitles.IndexOf(Title);
                    _activityTitles[index] = ConfirmedTitle;
                }

                return ConfirmedTitle;
            }
            catch (Exception e)
            {
                if (Response.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();
                else
                    throw new InvalidDataException();
            }
        }

        public async Task<ActivityTitleDto> GetTitleById(Guid id)
        {
            var Title = await HttpClient.GetFromJsonAsync<ActivityTitleDto>("titles/" + id.ToString());

            if (Title is null)
                throw new KeyNotFoundException();

            return Title;
        }

        public List<ActivityTitleDto> GetTitles()
        {
            return _activityTitles;
        }
    }
}
