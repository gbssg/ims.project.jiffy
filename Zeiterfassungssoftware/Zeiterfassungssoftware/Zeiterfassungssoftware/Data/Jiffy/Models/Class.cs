namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class Class
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<ShouldTime> ShouldTimes { get; set; } = new List<ShouldTime>();
}