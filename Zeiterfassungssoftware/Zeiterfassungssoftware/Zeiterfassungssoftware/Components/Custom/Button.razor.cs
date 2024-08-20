using Microsoft.AspNetCore.Components;

namespace Zeiterfassungssoftware.Components.Custom
{
    public partial class Button
    {
        public delegate void OnButtonClick();

        [Parameter]
        public string Text { get; set; } = string.Empty;
        [Parameter]
        public string Class { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<OnButtonClick> OnClick { get; set; }

        public string CssClass => Class.Trim().Equals("") ? "btn-primary" : Class;

        private void OnButtonClicked()
        {
            OnClick.InvokeAsync();
        }
    }
}