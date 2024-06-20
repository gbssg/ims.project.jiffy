using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Data.Filter;

namespace Zeiterfassungssoftware.Components
{
    public partial class TimeFilterComponent
    {
        [Parameter]
        public TimeFilter Filter { get; set; }
    }
}