using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.Times;
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
            BaseAddress = new Uri("https://localhost:7099/api/v1/users/")
        };
        public bool IsLoaded { get; set; }

        public List<UserDto> _users { get; set; } = new();

        public RemoteUserProvider()
        {
            LoadUsers();
        }

        public async void LoadUsers()
        {
            _users = await HttpClient.GetFromJsonAsync<List<UserDto>>("") ?? new();
            IsLoaded = true;
        }

        public async Task<UserDto> CreateUser(UserDto user)
        {
            var Response = await HttpClient.PostAsJsonAsync("", user);

            try
            {
                Response.EnsureSuccessStatusCode();
                var ReponseContent = await Response.Content.ReadAsStringAsync();
                var ConfirmedUser = JsonSerializer.Deserialize<UserDto>(ReponseContent, Options) ?? new();
                
                _users.Add(ConfirmedUser);
                return ConfirmedUser;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }

        public async Task DeleteUser(string id)
        {
            var Response = await HttpClient.DeleteAsync(id);

            try
            {
                Response.EnsureSuccessStatusCode();
                var User = _users.FirstOrDefault(e => e.Id == id);

                if (User is not null)
                    _users.Remove(User);
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<UserDto> GetUserById(string id)
        {
            var User = await HttpClient.GetFromJsonAsync<UserDto>(id);

            if (User is null)
                throw new KeyNotFoundException();

            return User;
        }

        public List<UserDto> GetUsers()
        {
            return _users;
        }

        public async Task<UserDto> UpdateUser(string id, UserDto user)
        {
            var Response = await HttpClient.PutAsJsonAsync(id, user);

            try
            {
                Response.EnsureSuccessStatusCode();

                var Body = await Response.Content.ReadAsStringAsync();
                var ConfirmedUser = JsonSerializer.Deserialize<UserDto>(Body, Options);

                if (ConfirmedUser is null)
                    throw new Exception();


                var User = _users.FirstOrDefault(e => e.Id == id);
                if (User is null)
                {
                    _users.Add(ConfirmedUser);
                }
                else
                {
                    var index = _users.IndexOf(User);
                    _users[index] = ConfirmedUser;
                }

                return ConfirmedUser;
            }
            catch (Exception e)
            {
                if (Response.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();
                else
                    throw new InvalidDataException();
            }
        }
    }
}
