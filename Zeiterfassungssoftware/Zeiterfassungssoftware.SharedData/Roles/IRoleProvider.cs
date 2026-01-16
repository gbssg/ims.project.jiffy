using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.SharedData.Classes;

namespace Zeiterfassungssoftware.SharedData.Roles
{
    public interface IRoleProvider
    {
        public bool IsLoaded { get; set; }

        public Task<RoleDto> CreateRole(RoleDto role);
        public Task<RoleDto> UpdateRole(string id, RoleDto role);
        public Task DeleteRole(string id);
        public Task<RoleDto> GetRoleById(string id);
        public List<RoleDto> GetRoles();

    }
}
