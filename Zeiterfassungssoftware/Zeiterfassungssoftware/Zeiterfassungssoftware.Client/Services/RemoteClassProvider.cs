using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.Time;

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
        public List<Class> _classes { get; set; } = new();
        
        public RemoteClassProvider()
        {
            _classes = HttpClient.GetFromJsonAsync<List<Class>>("").GetAwaiter().GetResult() ?? new();
            IsLoaded = true;
        }
        public void Add(Class Class)
        {
            string JsonData = JsonSerializer.Serialize(Class);
            var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage Response = HttpClient.PostAsync("", Content).GetAwaiter().GetResult();

            try
            {
                Response.EnsureSuccessStatusCode();
                string ReponseContent = Response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Class = JsonSerializer.Deserialize<Class>(ReponseContent, Options) ?? new();
                _classes.Add(Class);
            }
            catch (Exception e) { Console.WriteLine("Failed to Send Class"); }

            return;
        }

        public List<Class> GetClasses()
        {
            return _classes;
        }

        public async Task<Class> GetClassById(Guid Id)
        {
            return await HttpClient.GetFromJsonAsync<Class>($"{Id}");
        }

        public void Remove(Class Class)
        {
            HttpResponseMessage Message = HttpClient.DeleteAsync($"{Class.Id}").GetAwaiter().GetResult();

            try
            {
                Message.EnsureSuccessStatusCode();

                Class = _classes.FirstOrDefault(e => e.Id == Class.Id);
                if (Class is not null)
                    _classes.Remove(Class);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task Update(Class Class)
        {
            string JsonData = JsonSerializer.Serialize(Class);
            var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage Response = await HttpClient.PatchAsync("", Content);

            try
            {
                Response.EnsureSuccessStatusCode();
                _classes[_classes.IndexOf(_classes.FirstOrDefault(e => e.Id == Class.Id) ?? new())] = Class;
            }
            catch (Exception e) { Console.WriteLine("Failed to Update Class"); }

            return;
        }

    }
}
