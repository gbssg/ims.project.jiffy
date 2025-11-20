using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Client.Services
{
    public class RemoteClassProvider : IClassProvider
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        public HttpClient HttpClient { get; set; } = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7099/api/v1/classes/")
        };

        public bool IsLoaded { get; set; }
        public List<ClassDto> _classes { get; set; } = new();
        
        public RemoteClassProvider()
        {
            LoadClasses();
        }

        public async void LoadClasses()
        {
            _classes = await HttpClient.GetFromJsonAsync<List<ClassDto>>("") ?? new();
        }

        public async Task<ClassDto> CreateClass(ClassDto @class)
        {
            var Response = await HttpClient.PostAsJsonAsync("", @class);

            try
            {
                Response.EnsureSuccessStatusCode();
                var ReponseContent = await Response.Content.ReadAsStringAsync();
                var ConfirmedClass = JsonSerializer.Deserialize<ClassDto>(ReponseContent, Options) ?? new();

                _classes.Add(ConfirmedClass);
                return ConfirmedClass;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }

        public async Task<ClassDto> UpdateClass(Guid id, ClassDto @class)
        {
            var Response = await HttpClient.PutAsJsonAsync("", @class);

            try
            {
                Response.EnsureSuccessStatusCode();

                var Body = await Response.Content.ReadAsStringAsync();
                var ConfirmedClass = JsonSerializer.Deserialize<ClassDto>(Body);

                if (ConfirmedClass is null)
                    throw new Exception();

                var Class = _classes.FirstOrDefault(e => e.Id == id);
                if (Class is null)
                {
                    _classes.Add(ConfirmedClass);
                }
                else
                {
                    var index = _classes.IndexOf(Class);
                    _classes[index] = ConfirmedClass;
                }

                return ConfirmedClass;
            }
            catch (Exception e)
            {
                if (Response.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();
                else
                    throw new InvalidDataException();
            }
        }

        public async Task DeleteClass(Guid id)
        {
            var Response = await HttpClient.DeleteAsync(id.ToString());

            try
            {
                Response.EnsureSuccessStatusCode();
                var Class = _classes.FirstOrDefault(e => e.Id == id);

                if (Class is not null)
                    _classes.Remove(Class);
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ClassDto> GetClassById(Guid id)
        {
            var Class = await HttpClient.GetFromJsonAsync<ClassDto>(id.ToString());

            if (Class is null)
                throw new KeyNotFoundException();

            return Class;
        }

        public List<ClassDto> GetClasses()
        {
            return _classes;
        }

        public async Task<ClassDto> GetOwnClass()
        {
            var Class = await HttpClient.GetFromJsonAsync<ClassDto>("own");

            if (Class is null)
                throw new KeyNotFoundException();

            return Class;
        }
    }
}
