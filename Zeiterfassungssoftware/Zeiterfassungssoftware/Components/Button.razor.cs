using Microsoft.AspNetCore.Components;

namespace Zeiterfassungssoftware.Components
{
    public partial class Button
    {
        public delegate void OnButtonClick();

        [Parameter]
        public string Text { get; set; } = string.Empty;
        [Parameter]
        public string Width { get; set; } = "100%";
        [Parameter]
        public string CustomCssClass { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<OnButtonClick> OnClick { get; set; }

        public string CssClass => CustomCssClass.Trim().Equals("") ? "btn-primary" : CustomCssClass;

        private void OnButtonClicked()
        {
            OnClick.InvokeAsync();
        }
    }
}