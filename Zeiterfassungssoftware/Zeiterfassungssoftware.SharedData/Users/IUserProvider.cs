using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Users
{
    public interface IUserProvider
    {
        public void UpdateClass(Guid ClassId);
    }
}
