using System;
using System.Collections.Generic;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class Class
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public TimeSpan ShouldTimeMonday { get; set; }
    public TimeSpan ShouldTimeTuesday { get; set; }
    public TimeSpan ShouldTimeWednesday { get; set; }
    public TimeSpan ShouldTimeThursday { get; set; }
    public TimeSpan ShouldTimeFriday { get; set; }


}
