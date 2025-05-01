using Zeiterfassungssoftware.Controller;
using Zeiterfassungssoftware.Data.Jiffy.Models;

namespace Zeiterfassungssoftware.Mapper
{
    public class ClassMapper
    {
        public static Class FromDTO(SharedData.Classes.Class Class)
        {
            return new()
            {
                Id = Class.Id,
                Name = Class.Name,
                ShouldTimeMonday = Class.ShouldTimes[0],
                ShouldTimeTuesday = Class.ShouldTimes[1],
                ShouldTimeWednesday = Class.ShouldTimes[2],
                ShouldTimeThursday = Class.ShouldTimes[3],
                ShouldTimeFriday = Class.ShouldTimes[4]
            };
        }

        public static SharedData.Classes.Class ToDTO(Class Class)
        {
            var ShouldTimes = new TimeSpan[5]
            {
                Class.ShouldTimeMonday,
                Class.ShouldTimeTuesday,
                Class.ShouldTimeWednesday,
                Class.ShouldTimeThursday,
                Class.ShouldTimeFriday
            };

            return new SharedData.Classes.Class()
            {
                Id = Class.Id,
                Name = Class.Name,
                ShouldTimes = ShouldTimes
            };
        }

        public static bool ValidateDTO(SharedData.Classes.Class Class)
        {
            return (!string.IsNullOrEmpty(Class.Name) && (Class.ShouldTimes.Length == 5));
        }
    }
}
