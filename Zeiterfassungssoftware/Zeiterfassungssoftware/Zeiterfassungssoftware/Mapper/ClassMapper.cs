using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Classes;

namespace Zeiterfassungssoftware.Mapper
{
    public class ClassMapper
    {
        public static Class FromDTO(ClassDto classDto)
        {
            if (classDto is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = classDto.Id,
                Name = classDto.Name,
                ShouldTimes = classDto.ShouldTimes.Select(e => ShouldTimeMapper.FromDTO(e)).ToList()
            };
        }

        public static ClassDto ToDTO(Class _class)
        {
            if (_class is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = _class.Id,
                Name = _class.Name,
                ShouldTimes = _class.ShouldTimes.Select(e => ShouldTimeMapper.ToDTO(e)).ToList()
            };
        }

        public static bool ValidateDTO(ClassDto classDto)
        {
            if (classDto is null)
                return false;

            return (!string.IsNullOrWhiteSpace(classDto.Name));
        }
    }
}
