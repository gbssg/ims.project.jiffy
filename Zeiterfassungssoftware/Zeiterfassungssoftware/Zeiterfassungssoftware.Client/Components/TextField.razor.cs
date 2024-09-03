using Microsoft.AspNetCore.Components;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class TextField
    {


        [Parameter]
        public string PlaceHolder { get; set; } = string.Empty;

        [Parameter]
        public string Value { get; set; } = string.Empty;

        [Parameter]
        public string Class { get; set; } = "textfield";

        [Parameter]
        public string Style { get; set; } = string.Empty;


        [Parameter]
        public bool Multiline { get; set; }


        [Parameter] public EventCallback<string> ValueChanged { get; set; }


        private void OnTextChanged(string value)
        {
            Value = value;
            ValueChanged.InvokeAsync(value);
        }
    }
}