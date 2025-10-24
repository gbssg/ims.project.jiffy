﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class Entry
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }
    public DateTime? End { get; set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public Guid ShouldTimeId { get; set; }
    public virtual ShouldTime ShouldTime { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;
    
}
