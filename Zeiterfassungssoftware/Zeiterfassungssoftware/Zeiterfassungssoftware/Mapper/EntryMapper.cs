using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Mapper
{
    public class EntryMapper
    {

        public static Entry FromDTO(TimeEntryDto timeEntryDto)
        {
            if (timeEntryDto is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = timeEntryDto.Id,
                Start = timeEntryDto.Start,
                End = timeEntryDto.End,
                Title = timeEntryDto.Title,
                Description = timeEntryDto.Description
            };
        }

        public static TimeEntryDto ToDTO(Entry entry)
        {
            if (entry is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = entry.Id,
                Start = entry.Start,
                End = entry.End,
                Title = entry.Title,
                Description = entry.Description,
                ShouldTime = entry.ShouldTime.Should
            };
        }

        public static bool ValidateDTO(TimeEntryDto timeEntryDto)
        {
            if (timeEntryDto is null)
                return false;

            if (string.IsNullOrWhiteSpace(timeEntryDto.Title) || string.IsNullOrWhiteSpace(timeEntryDto.Description))
                return false;

            if ((timeEntryDto.End is not null) && timeEntryDto.Start >= timeEntryDto.End)
                return false;

            return true;
        }
    }
}
