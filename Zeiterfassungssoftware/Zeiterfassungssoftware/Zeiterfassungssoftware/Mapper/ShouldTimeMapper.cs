using Zeiterfassungssoftware.Data.Jiffy.Models;

namespace Zeiterfassungssoftware.Mapper
{
    public class ShouldTimeMapper
    {
        public static ShouldTime FromDTO(SharedData.Classes.ShouldTime ShouldTime)
        {
            if (ShouldTime is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = ShouldTime.Id,
                ClassId = ShouldTime.ClassId,
                Should = ShouldTime.Should,
                DayOfWeek = ShouldTime.DayOfWeek,
            };
        }

        public static SharedData.Classes.ShouldTime ToDTO(ShouldTime ShouldTime)
        {
            if (ShouldTime is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = ShouldTime.Id,
                ClassId = ShouldTime.ClassId,
                Should = ShouldTime.Should,
                DayOfWeek = ShouldTime.DayOfWeek,
            };
        }
    }
}
