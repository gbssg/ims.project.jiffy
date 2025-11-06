using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Zeiterfassungssoftware.Data.Jiffy.Models;

namespace Zeiterfassungssoftware.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ActivityDescription> ActivityDescriptions { get; set; } = new List<ActivityDescription>();

        public virtual ICollection<ActivityTitle> ActivityTitles { get; set; } = new List<ActivityTitle>();

        public virtual ICollection<Entry> Entries { get; set; } = new List<Entry>();
        public Guid ClassId { get; set; }
        public virtual Class Class { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if(obj is not ApplicationUser other) 
                return false;

            return this.Id == other.Id;
        }
    }

}
