using System.Text.Json;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Client.Services
{
    public class RemoteUserProvider : IUserProvider
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        public HttpClient HttpClient { get; set; } = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7099/api/v1/user/")
        };

        public async void UpdateClass(Guid ClassId)
        {
            HttpResponseMessage Response = await HttpClient.PatchAsync($"set-class/{ClassId}", null);
            try
            {
                Response.EnsureSuccessStatusCode();
            }
            catch (Exception e) 
            { 
                Console.WriteLine($"Failed to Update Class: {e.Message}"); 
            }

            return;
        }
    }
}
