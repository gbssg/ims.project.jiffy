namespace Zeiterfassungssoftware.Data.Jiffy.Models
{
    public partial class ShouldTime
    {
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Should { get; set; }
        public DateTime ValidUntil { get; set; }
        public Guid ClassId { get; set; }
        public virtual Class Class { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is not ShouldTime other)
                return false;

            return this.Id == other.Id;
        }
    }
}
