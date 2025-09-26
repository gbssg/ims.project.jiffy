using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.SharedData.User;

namespace Zeiterfassungssoftware.Mapper
{
    public class UserMapper
    {
        public static User ToDTO(ApplicationUser applicationUser)
        {
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

        public static ApplicationUser FromDTO(User user)
        {
            return new()
            {
                Id = user.Id,
                ClassId = user.ClassId,
                UserName = user.UserName,
                NormalizedUserName = user.NormalizedUserName,
                Email = user.Email,
                NormalizedEmail = user.NormalizedEmail,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                TwoFactorEnabled = user.TwoFactorEnabled,
                LockoutEnd = user.LockoutEnd == DateTime.MinValue ? null : new DateTimeOffset(user.LockoutEnd),
                LockoutEnabled = user.LockoutEnabled,
                AccessFailedCount = user.AccessFailedCount
            };
        }
    }
}
