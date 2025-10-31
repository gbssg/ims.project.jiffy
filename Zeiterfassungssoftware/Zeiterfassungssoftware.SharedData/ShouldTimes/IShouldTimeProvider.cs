using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.SharedData.Classes;

namespace Zeiterfassungssoftware.SharedData.ShouldTimes
{
    public interface IShouldTimeProvider
    {
        public bool IsLoaded { get; set; }
        public void Add(ShouldTime ShouldTime);
        public void Remove(ShouldTime ShouldTime);
        public ShouldTime GetShouldTimeById(ShouldTime Id);
        public List<ShouldTime> GetShouldTimes();
        public Task Update(ShouldTime ShouldTime);

    }
}
