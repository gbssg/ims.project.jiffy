using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Client.Services
{
    public class RemoteShouldTimeProvider : IShouldTimeProvider
    {
        public bool IsLoaded { get; set; }

        public void Add(ShouldTime ShouldTime)
        {

        }

        public Task<ShouldTime> GetShouldTimeById(ShouldTime Id)
        {
            return null!;
        }

        public List<ShouldTime> GetShouldTimes()
        {
            return new();
        }

        public void Remove(ShouldTime Entry)
        {

        }

        public async Task Update(ShouldTime ShouldTime)
        {
            
        }
    }
}
