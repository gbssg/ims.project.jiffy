using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Client.Data.Filter;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class DateFilterComponent
    {

        [Parameter]
        public DateFilter Filter { get; set; }

    }
}