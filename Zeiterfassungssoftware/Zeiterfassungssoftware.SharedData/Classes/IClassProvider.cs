using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.SharedData.Classes
{
    public interface IClassProvider
    {
        public bool IsLoaded { get; set; }
        public void Add(Class Class);
        public void Remove(Class Entry);
        public Task<Class> GetClassById(Guid Id);
        public List<Class> GetClasses();
        public Task Update(Class Class);
        public Task<Class> GetOwnClass();

    }
}
