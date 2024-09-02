using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Client.Data.Filter;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class TimeFilterComponent
    {
        [Parameter]
        public TimeFilter Filter { get; set; } = new("");
    }
}