namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class ActivityTitle
{
    public Guid Id { get; set; }

    public bool Favorite { get; set; }
    public string Title { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;


    public override bool Equals(object? obj)
    {
        if (obj is not ActivityTitle other)
            return false;

        return this.Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
