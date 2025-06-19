using Zeiterfassungssoftware.Data.Jiffy.Models;

namespace Zeiterfassungssoftware.Mapper
{
    public class ShouldTimeMapper
    {
        public static ShouldTime FromDTO(SharedData.Classes.ShouldTime ShouldTime)
        {
            return new()
            {
                ClassId = ShouldTime.ClassId,
                Should = ShouldTime.Should,
                DayOfWeek = ShouldTime.DayOfWeek,
            };
        }

        public static SharedData.Classes.ShouldTime ToDTO(ShouldTime ShouldTime)
        {
            return new()
            {
                ClassId = ShouldTime.ClassId,
                Should = ShouldTime.Should,
                DayOfWeek = ShouldTime.DayOfWeek,
            };
        }
    }
}
