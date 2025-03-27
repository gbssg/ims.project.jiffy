using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class Activity
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public bool Favorite { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;

    public ActivityTitle ToActivityTitle()
    {
        return new ActivityTitle()
        {
            Id = this.Id,
            Value = this.Title,
            Favorite = this.Favorite,
        };
    }

}
