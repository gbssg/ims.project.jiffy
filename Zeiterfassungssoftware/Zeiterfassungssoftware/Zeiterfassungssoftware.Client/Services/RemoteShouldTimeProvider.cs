using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.ShouldTimes;
using Zeiterfassungssoftware.SharedData.Times;

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

        public async Task<ShouldTimeDto> CreateShouldTime(ShouldTimeDto shouldTime)
        {
            var Response = await HttpClient.PostAsJsonAsync("", shouldTime);

            try
            {
                Response.EnsureSuccessStatusCode();
                var ReponseContent = await Response.Content.ReadAsStringAsync();
                var ConfirmedShouldTime = JsonSerializer.Deserialize<ShouldTimeDto>(ReponseContent, Options) ?? new();
                
                _shouldTimes.Add(ConfirmedShouldTime);
                return ConfirmedShouldTime;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }

        public async Task<ShouldTimeDto> UpdateShouldTime(Guid id, ShouldTimeDto souldTime)
        {
            var Response = await HttpClient.PutAsJsonAsync(id.ToString(), souldTime);

            try
            {
                Response.EnsureSuccessStatusCode();

                var Body = await Response.Content.ReadAsStringAsync();
                var ConfirmedShouldTime = JsonSerializer.Deserialize<ShouldTimeDto>(Body);

                if (ConfirmedShouldTime is null)
                    throw new Exception();


                var ShouldTime = _shouldTimes.FirstOrDefault(e => e.Id == id);
                if (ShouldTime is null)
                {
                    _shouldTimes.Add(ConfirmedShouldTime);
                }
                else
                {
                    var index = _shouldTimes.IndexOf(ShouldTime);
                    _shouldTimes[index] = ConfirmedShouldTime;
                }

                return ConfirmedShouldTime;
            }
            catch (Exception e)
            {
                if (Response.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();
                else
                    throw new InvalidDataException();
            }
        }

        public async Task DeleteShouldTime(Guid id)
        {
            var Response = await HttpClient.DeleteAsync(id.ToString());

            try
            {
                Response.EnsureSuccessStatusCode();
                var ShouldTime = _shouldTimes.FirstOrDefault(e => e.Id == id);

                if (ShouldTime is not null)
                    _shouldTimes.Remove(ShouldTime);
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<ShouldTimeDto> GetShouldTimeById(Guid id)
        {
            var ShouldTime = await HttpClient.GetFromJsonAsync<ShouldTimeDto>(id.ToString());

            if (ShouldTime is null)
                throw new KeyNotFoundException();

            return ShouldTime;
        }

        public List<ShouldTimeDto> GetShouldTimes()
        {
            return _shouldTimes;
        }
    }
}
