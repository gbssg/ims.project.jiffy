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
            LoadData();
        }

        public async void LoadData()
        {
            _classes = await HttpClient.GetFromJsonAsync<List<Class>>("") ?? new();
            IsLoaded = true;
        }

        public async void Add(Class Class)
        {
            string JsonData = JsonSerializer.Serialize(Class);
            var Content = new StringContent(JsonData, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage Response = await HttpClient.PostAsync("", Content);
                Response.EnsureSuccessStatusCode();
                string ReponseContent = await Response.Content.ReadAsStringAsync();
                Class = JsonSerializer.Deserialize<Class>(ReponseContent, Options) ?? new();
                _classes.Add(Class);
            }
            catch (Exception e) { 
                Console.WriteLine(e.Message);
            }

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

        public async void Remove(Class Class)
        {

            try
            {
                HttpResponseMessage Message = await HttpClient.DeleteAsync($"{Class.Id}");
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

        public async Task<Class> GetOwnClass()
        {
            return await HttpClient.GetFromJsonAsync<Class>($"own") ?? new();
        }
    }
}
