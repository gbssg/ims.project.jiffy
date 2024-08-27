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
        public string AdditionalStyling { get; set; } = string.Empty;


        private string Class => $"btn-round {(Disabled ? "btn-disabled" : Started ? "btn-started" : "btn-stopped")}";
        private string Text => Started ? "Stop" : "Start";

        private void OnButtonClicked()
        {
            OnClick.InvokeAsync();
        }
    }
}