using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Components.Data.Filter;

namespace Zeiterfassungssoftware.Components.Custom
{
    public partial class StringFilterComponent
    {
        [Parameter]
        public StringFilter Filter { get; set; } = new("");
    }
}