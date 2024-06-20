using Microsoft.AspNetCore.Components;

namespace Zeiterfassungssoftware.Components
{
    public partial class Message
    {
        [Parameter]
        public string Title { get; set; } = string.Empty;
        [Parameter]
        public string Text { get; set; } = string.Empty;
        [Parameter]
        public string Color { get; set; } = string.Empty;
    }
}