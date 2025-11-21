namespace Zeiterfassungssoftware.SharedData.Activities
{
	public interface IActivityProvider
	{
		public bool IsLoaded { get; }

		public Task DeleteDescription(Guid id);
		public Task<ActivityDescriptionDto> CreateDescription(ActivityDescriptionDto activityDescription);
        public Task<ActivityDescriptionDto> UpdateDescription(Guid id, ActivityDescriptionDto activityDescription);
		public Task<ActivityDescriptionDto> GetDescriptionById(Guid id);
        public List<ActivityDescriptionDto> GetDescriptions();



        public Task DeleteTitle(Guid id);
        public Task<ActivityTitleDto> CreateTitle(ActivityTitleDto activityTitle);
        public Task<ActivityTitleDto> UpdateTitle(Guid id, ActivityTitleDto activityTitle);
        public Task<ActivityTitleDto> GetTitleById(Guid id);
        public List<ActivityTitleDto> GetTitles();

    }
}
