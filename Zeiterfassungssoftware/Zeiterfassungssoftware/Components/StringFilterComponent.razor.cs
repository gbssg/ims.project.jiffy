using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Data.Filter;

namespace Zeiterfassungssoftware.Components
{
    public partial class StringFilterComponent
    {
        [Parameter]
        public StringFilter Filter { get; set; }

    }
}