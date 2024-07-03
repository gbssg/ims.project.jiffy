using Microsoft.AspNetCore.Components;

namespace Zeiterfassungssoftware.Components
{
    public partial class TextField
    {

        [Parameter]
        public string PlaceHolder { get; set; } = string.Empty;

        [Parameter]
        public string Value { get; set; } = string.Empty;

        [Parameter] public EventCallback<string> ValueChanged { get; set; }


        private void OnTextChanged(string value)
        {
            Value = value;
            ValueChanged.InvokeAsync(value);
        }
    }
}