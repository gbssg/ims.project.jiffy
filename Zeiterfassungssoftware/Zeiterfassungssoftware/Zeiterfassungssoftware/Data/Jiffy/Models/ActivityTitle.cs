namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class ActivityTitle
{
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;
}
