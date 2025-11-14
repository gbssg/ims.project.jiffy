namespace Zeiterfassungssoftware.SharedData.Users
{
    public interface IUserProvider
    {
        public bool IsLoaded { get; set; }

        public Task DeleteUser(string id);
        public Task<UserDto> CreateUser(UserDto user);
        public Task<UserDto> UpdateUser(string id, UserDto user);
        public List<UserDto> GetUsers();
        public Task<UserDto> GetUserById(string id);
    }
}
