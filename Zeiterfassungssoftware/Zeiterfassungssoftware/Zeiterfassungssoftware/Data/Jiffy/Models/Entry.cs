using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Data.Jiffy.Models;

public partial class Entry
{
    public Guid Id { get; set; }

    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public TimeSpan ShouldTime { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ApplicationUser User { get; set; } = null!;

    public TimeEntry ToTimeEntry(string UserName)
    {
        return new TimeEntry()
        {
            Id = this.Id,
            Start = this.Start,
            End = this.End,
            Title = this.Title,
            Description = this.Description,
            Username = UserName,
            ShouldTime = this.ShouldTime
        };
    }

    
}
