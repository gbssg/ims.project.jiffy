using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.ShouldTimes
{
    public class ShouldTime
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Should { get; set; }
    }
}
