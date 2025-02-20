using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class ActivityDescription
{
    [JsonIgnore]
    public Guid Id { get; set; }

    public string Value { get; set; } = null!;

    [JsonIgnore]
    public string UserId { get; set; } = null!;

    [JsonIgnore]
    public virtual ApplicationUser User { get; set; } = null!;
}
