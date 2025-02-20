using Microsoft.AspNetCore.Identity;
using Zeiterfassungssoftware.Data.Jiffy.Models;

namespace Zeiterfassungssoftware.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ActivityDescription> ActivityDescriptions { get; set; } = new List<ActivityDescription>();

        public virtual ICollection<Activity> ActivityTitles { get; set; } = new List<Activity>();

        public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
    }

}
