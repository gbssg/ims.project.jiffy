using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Client.Data.Filter;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class StringFilterComponent
    {
        [Parameter]
        public StringFilter Filter { get; set; } = new("");
    }
}