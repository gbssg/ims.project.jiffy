using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Mapper
{
    public class ShouldTimeMapper
    {
        public static ShouldTime FromDTO(ShouldTimeDto shouldTimeDto)
        {
            if (shouldTimeDto is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = shouldTimeDto.Id,
                ClassId = shouldTimeDto.ClassId,
                Should = shouldTimeDto.Should,
                DayOfWeek = shouldTimeDto.DayOfWeek,
            };
        }

        public static ShouldTimeDto ToDTO(ShouldTime shouldTime)
        {
            if (shouldTime is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = shouldTime.Id,
                ClassId = shouldTime.ClassId,
                Should = shouldTime.Should,
                DayOfWeek = shouldTime.DayOfWeek,
            };
        }

        public static bool ValidateDto(ShouldTimeDto shouldTimeDto)
        {
            if (shouldTimeDto is null)
                return false;

            if(shouldTimeDto.Should <= TimeSpan.FromSeconds(0))
                return false;

            return true;
        }
    }
}
