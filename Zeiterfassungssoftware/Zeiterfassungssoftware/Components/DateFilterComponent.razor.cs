using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Data.Filter;

namespace Zeiterfassungssoftware.Components
{
    public partial class DateFilterComponent
    {
        [Parameter]
        public DateFilter Filter { get; set; }
    }
}