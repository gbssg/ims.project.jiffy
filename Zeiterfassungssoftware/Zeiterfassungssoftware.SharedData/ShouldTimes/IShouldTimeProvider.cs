namespace Zeiterfassungssoftware.SharedData.ShouldTimes
{
    public interface IShouldTimeProvider
    {
        public bool IsLoaded { get; set; }

        public Task<ShouldTimeDto> CreateShouldTime(ShouldTimeDto shouldTime);
        public Task<ShouldTimeDto> UpdateShouldTime(Guid id, ShouldTimeDto shouldTime);
        public Task DeleteShouldTime(Guid id);
        public Task<ShouldTimeDto> GetShouldTimeById(Guid id);
        public List<ShouldTimeDto> GetShouldTimes();

    }
}
