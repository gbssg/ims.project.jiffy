using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.Roles;

namespace Zeiterfassungssoftware.Client.Services
{
    public class RemoteRoleProvider : IRoleProvider
    {
        public static readonly JsonSerializerOptions Options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        public HttpClient HttpClient { get; set; } = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7099/api/v1/roles/")
        };

        public bool IsLoaded { get; set; }
        public List<RoleDto> _roles { get; set; } = new();

        public RemoteRoleProvider()
        {
            LoadRoles();
        }

        public async void LoadRoles()
        {
            _roles = await HttpClient.GetFromJsonAsync<List<RoleDto>>("") ?? new();
            IsLoaded = true;
        }

        public async Task<RoleDto> CreateRole(RoleDto role)
        {
            var Response = await HttpClient.PostAsJsonAsync("", role);

            try
            {
                Response.EnsureSuccessStatusCode();
                var ReponseContent = await Response.Content.ReadAsStringAsync();
                var ConfirmedRole = JsonSerializer.Deserialize<RoleDto>(ReponseContent, Options) ?? new();

                _roles.Add(ConfirmedRole);
                return ConfirmedRole;
            }
            catch (Exception e)
            {
                throw new InvalidDataException();
            }
        }

        public async Task<RoleDto> UpdateRole(string id, RoleDto role)
        {
            var Response = await HttpClient.PutAsJsonAsync(id.ToString(), role);

            try
            {
                Response.EnsureSuccessStatusCode();

                var Body = await Response.Content.ReadAsStringAsync();
                var ConfirmedRole = JsonSerializer.Deserialize<RoleDto>(Body, Options);

                if (ConfirmedRole is null)
                    throw new Exception();

                var Role = _roles.FirstOrDefault(e => e.Id == id);
                if (Role is null)
                {
                    _roles.Add(ConfirmedRole);
                }
                else
                {
                    var index = _roles.IndexOf(Role);
                    _roles[index] = ConfirmedRole;
                }

                return ConfirmedRole;
            }
            catch (Exception e)
            {
                if (Response.StatusCode == HttpStatusCode.NotFound)
                    throw new KeyNotFoundException();
                else
                    throw new InvalidDataException();
            }
        }

        public async Task DeleteRole(string id)
        {
            var Response = await HttpClient.DeleteAsync(id.ToString());

            try
            {
                Response.EnsureSuccessStatusCode();
                var Role = _roles.FirstOrDefault(e => e.Id == id);

                if (Role is not null)
                    _roles.Remove(Role);
            }
            catch (Exception e)
            {
                throw new KeyNotFoundException();
            }
        }

        public async Task<RoleDto> GetRoleById(string id)
        {
            var Role = await HttpClient.GetFromJsonAsync<RoleDto>(id.ToString());

            if (Role is null)
                throw new KeyNotFoundException();

            return Role;
        }

        public List<RoleDto> GetRoles()
        {
            return _roles;
        }

    }
}
