using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Mapper
{
    public class UserMapper
    {
        public static UserDto ToDTO(ApplicationUser applicationUser)
        {
            if (applicationUser is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = applicationUser.Id,
                ClassId = applicationUser.ClassId,
                UserName = applicationUser.UserName ?? string.Empty,
                NormalizedUserName = applicationUser.NormalizedUserName ?? string.Empty,
                Email = applicationUser.Email ?? string.Empty,
                NormalizedEmail = applicationUser.NormalizedEmail ?? string.Empty,
                EmailConfirmed = applicationUser.EmailConfirmed,
                Password = string.Empty,
                PhoneNumber = applicationUser.PhoneNumber ?? string.Empty,
                PhoneNumberConfirmed = applicationUser.PhoneNumberConfirmed,
                TwoFactorEnabled = applicationUser.TwoFactorEnabled,
                LockoutEnd = applicationUser.LockoutEnd?.UtcDateTime ?? DateTime.MinValue,
                LockoutEnabled = applicationUser.LockoutEnabled,
                AccessFailedCount = applicationUser.AccessFailedCount
            };
        }

        public static ApplicationUser FromDTO(UserDto userDto)
        {
            if (userDto is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = userDto.Id,
                ClassId = userDto.ClassId,
                UserName = userDto.UserName,
                NormalizedUserName = userDto.NormalizedUserName,
                Email = userDto.Email,
                NormalizedEmail = userDto.NormalizedEmail,
                EmailConfirmed = userDto.EmailConfirmed,
                PhoneNumber = userDto.PhoneNumber,
                PhoneNumberConfirmed = userDto.PhoneNumberConfirmed,
                TwoFactorEnabled = userDto.TwoFactorEnabled,
                LockoutEnd = userDto.LockoutEnd == DateTime.MinValue ? null : new DateTimeOffset(user.LockoutEnd),
                LockoutEnabled = userDto.LockoutEnabled,
                AccessFailedCount = userDto.AccessFailedCount
            };
        }
    }
}
