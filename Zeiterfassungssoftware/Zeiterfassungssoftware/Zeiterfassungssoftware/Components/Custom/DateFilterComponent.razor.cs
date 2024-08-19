using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Components.Data.Filter;

namespace Zeiterfassungssoftware.Components.Custom
{
    public partial class DateFilterComponent
    {

        [Parameter]
        public DateFilter Filter { get; set; }

    }
}