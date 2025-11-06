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
        public void Add(ShouldTimeDto ShouldTime);
        public void Remove(ShouldTimeDto ShouldTime);
        public ShouldTimeDto GetShouldTimeById(ShouldTimeDto Id);
        public List<ShouldTimeDto> GetShouldTimes();
        public Task Update(ShouldTimeDto ShouldTime);

    }
}
