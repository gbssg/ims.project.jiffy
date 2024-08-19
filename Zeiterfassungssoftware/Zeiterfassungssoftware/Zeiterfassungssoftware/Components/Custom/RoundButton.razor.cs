using Microsoft.AspNetCore.Components;

namespace Zeiterfassungssoftware.Components.Custom
{
    public partial class RoundButton
    {
        public delegate void OnButtonClick();

        [Parameter]
        public EventCallback<OnButtonClick> OnClick { get; set; }

        [Parameter]
        public bool Started { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public string AdditionalStyling { get; set; }


        private string CssClass => Disabled ? "btn-disabled" : Started ? "btn-started" : "btn-round";
        private string Text => Started ? "Stop" : "Start";

        private void OnButtonClicked()
        {
            OnClick.InvokeAsync();
        }
    }
}