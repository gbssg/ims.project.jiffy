namespace Zeiterfassungssoftware.SharedData.Classes
{
    public interface IClassProvider
    {
        public bool IsLoaded { get; set; }

        public Task<ClassDto> CreateClass(ClassDto @class);
        public Task<ClassDto> UpdateClass(Guid id, ClassDto @class);
        public Task DeleteClass(Guid id);
        public Task<ClassDto> GetClassById(Guid id);
        public List<ClassDto> GetClasses();
        public Task<ClassDto> GetOwnClass();

    }
}
