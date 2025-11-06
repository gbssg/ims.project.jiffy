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
        public void Add(ClassDto Class);
        public void Remove(ClassDto Entry);
        public Task<ClassDto> GetClassById(Guid Id);
        public List<ClassDto> GetClasses();
        public Task Update(ClassDto Class);
        public Task<ClassDto> GetOwnClass();

    }
}
