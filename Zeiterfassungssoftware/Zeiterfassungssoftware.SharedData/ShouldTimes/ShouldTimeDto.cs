using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.SharedData.ShouldTimes
{
    public class ShouldTimeDto
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Should { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is ShouldTimeDto other)
                return other.Id == this.Id;
            return false;
        }

    }
}
