using Microsoft.AspNetCore.Identity;
using Zeiterfassungssoftware.SharedData.Roles;

namespace Zeiterfassungssoftware.Mapper
{
    public class RoleMapper
    {
        public static RoleDto ToDto(IdentityRole identityRole)
        {
            if (identityRole is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = identityRole.Id,
                Name = identityRole.Name ?? "",
                NormalizedName = identityRole.NormalizedName ?? "",
                ConcurrencyStamp = identityRole.ConcurrencyStamp ?? ""
            };
        }

        public static IdentityRole FromDto(RoleDto roleDto)
        {
            if (roleDto is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                NormalizedName = roleDto.NormalizedName,
                ConcurrencyStamp = roleDto.ConcurrencyStamp
            };
        }
    }
}
