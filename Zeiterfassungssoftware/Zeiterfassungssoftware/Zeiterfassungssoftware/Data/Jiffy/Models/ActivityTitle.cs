namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class ActivityTitle
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
