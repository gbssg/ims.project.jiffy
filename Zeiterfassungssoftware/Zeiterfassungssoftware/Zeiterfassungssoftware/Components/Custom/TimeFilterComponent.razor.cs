using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Components.Data.Filter;

namespace Zeiterfassungssoftware.Components.Custom
{
    public partial class TimeFilterComponent
    {
        [Parameter]
        public TimeFilter Filter { get; set; } = new("");
    }
}