using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class ActivityTitle
{
    public Guid Id { get; set; }

    public bool Favorite { get; set; }
    public string Title { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;
}
