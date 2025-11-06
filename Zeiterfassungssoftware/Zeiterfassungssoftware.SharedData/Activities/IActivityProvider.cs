namespace Zeiterfassungssoftware.SharedData.Activities
{
	public interface IActivityProvider
	{
		public bool IsLoaded { get; }

		public void Remove(object Obj);
		public void Add(object Obj);
		public bool Contains(object Obj);
        public Task<object> Update(object Obj);

        public List<ActivityDescriptionDto> GetActivityDescriptions();
		public List<ActivityTitleDto> GetActivityTitles();
	}
}
