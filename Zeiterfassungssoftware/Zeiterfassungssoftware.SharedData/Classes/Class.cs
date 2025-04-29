using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Classes
{
    public class Class
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public TimeSpan[] ShouldTimes { get; set; } = new TimeSpan[5];
    }
}
