using System.Net.Http.Json;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Client.Services
{
    public class RemoteShouldTimeProvider : IShouldTimeProvider
    {
        public bool IsLoaded { get; set; }
        public List<ShouldTimeDto> _shouldTimes { get; set; }

        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        public HttpClient HttpClient { get; set; } = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7099/api/v1/shouldtimes/")
        };


        public RemoteShouldTimeProvider() { 
            LoadShouldTimes();
        }

        public async Task LoadShouldTimes()
        {
            _shouldTimes = await HttpClient.GetFromJsonAsync<List<ShouldTimeDto>>("") ?? new();
        }

        public void Add(ShouldTimeDto ShouldTime)
        {
            var Result = HttpClient.PostAsJsonAsync("", ShouldTime).GetAwaiter().GetResult();

            if(!Result.IsSuccessStatusCode)
                throw new Exception("Failed to add new shouldtime");
            

            var Response = Result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var ConfirmedShouldTime = JsonSerializer.Deserialize<ShouldTimeDto>(Response, Options);
            _shouldTimes.Add(ConfirmedShouldTime);
        }

        public ShouldTimeDto GetShouldTimeById(ShouldTimeDto Id)
        {
            var ShouldTime = HttpClient.GetFromJsonAsync<ShouldTimeDto>($"{Id}").GetAwaiter().GetResult() ?? new();
            return ShouldTime;
        }

        public List<ShouldTimeDto> GetShouldTimes()
        {
            return _shouldTimes;
        }

        public void Remove(ShouldTimeDto ShouldTime)
        {
            var Result = HttpClient.DeleteAsync($"{ShouldTime.Id}").GetAwaiter().GetResult();

            if (!Result.IsSuccessStatusCode)
                throw new Exception("Failed to delete shouldtime");
            
            _shouldTimes.Remove(ShouldTime);
        }

        public async Task Update(ShouldTimeDto ShouldTime)
        {
            var Result = await HttpClient.PutAsJsonAsync($"{ShouldTime.Id}", ShouldTime);

            if (!Result.IsSuccessStatusCode)
                throw new Exception("Failed to update shouldtime");
            
            var Response = await Result.Content.ReadAsStringAsync();
            var UpdatedShouldTime = JsonSerializer.Deserialize<ShouldTimeDto>(Response, Options);
            _shouldTimes[_shouldTimes.IndexOf(ShouldTime)] = UpdatedShouldTime ?? new();
        }
    }
}
