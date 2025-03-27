using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class ActivityDescription
{
    public Guid Id { get; set; }
    public string Value { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public bool Favorite { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;

    public SharedData.Activities.ActivityDescription ToActivityDescription()
    {
        return new SharedData.Activities.ActivityDescription
        {
            Id = this.Id,
            Value = this.Value,
            Favorite = this.Favorite,
        };
    }
}
