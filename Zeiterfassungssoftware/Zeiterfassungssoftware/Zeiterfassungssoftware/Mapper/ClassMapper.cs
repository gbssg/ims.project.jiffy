using Zeiterfassungssoftware.Controller;
using Zeiterfassungssoftware.Data.Jiffy.Models;

namespace Zeiterfassungssoftware.Mapper
{
    public class ClassMapper
    {
        public static Class FromDTO(SharedData.Classes.Class Class)
        {
            if (Class is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = Class.Id,
                Name = Class.Name,
                ShouldTimes = Class.ShouldTimes.Select(e => ShouldTimeMapper.FromDTO(e)).ToList()
            };
        }

        public static SharedData.Classes.Class ToDTO(Class Class)
        {
            if (Class is null)
                throw new ArgumentNullException();

            return new SharedData.Classes.Class()
            {
                Id = Class.Id,
                Name = Class.Name,
                ShouldTimes = Class.ShouldTimes.Select(e => ShouldTimeMapper.ToDTO(e)).ToList()
            };
        }

        public static bool ValidateDTO(SharedData.Classes.Class Class)
        {
            if (Class is null)
                return false;

            return (!string.IsNullOrEmpty(Class.Name));
        }
    }
}
