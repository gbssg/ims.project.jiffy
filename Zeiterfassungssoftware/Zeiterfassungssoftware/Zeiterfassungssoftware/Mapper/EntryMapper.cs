using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Mapper
{
    public class EntryMapper
    {

        // NOTE This does not set the user id
        public static Entry FromDTO(TimeEntry TimeEntry)
        {
            if (TimeEntry is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = TimeEntry.Id,
                Start = TimeEntry.Start,
                End = TimeEntry.End,
                Title = TimeEntry.Title,
                Description = TimeEntry.Description
            };
        }

        // NOTE this does not set the username
        public static TimeEntry ToDTO(Entry Entry)
        {
            if (Entry is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = Entry.Id,
                Start = Entry.Start,
                End = Entry.End,
                Title = Entry.Title,
                Description = Entry.Description,
                ShouldTime = Entry.ShouldTime.Should
            };
        }

        public static bool ValidateDTO(TimeEntry Entry)
        {
            if (Entry is null)
                return false;

            if (string.IsNullOrWhiteSpace(Entry.Title) || string.IsNullOrWhiteSpace(Entry.Description))
                return false;

            if ((Entry.End is not null) && Entry.Start >= Entry.End)
                return false;

            return true;
        }
    }
}
