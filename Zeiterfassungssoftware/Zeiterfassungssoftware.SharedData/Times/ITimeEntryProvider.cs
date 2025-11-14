namespace Zeiterfassungssoftware.SharedData.Times
{
    public interface ITimeEntryProvider
    {
        public bool IsLoaded { get; }

        public Task<TimeEntryDto> CreateEntry(TimeEntryDto entry);
		public Task DeleteEntry(Guid id);
        public Task<TimeEntryDto?> GetEntryById(Guid id);
        public List<TimeEntryDto> GetEntries();
        public Task<TimeEntryDto> UpdateEntry(Guid id, TimeEntryDto entry);

    }
}
