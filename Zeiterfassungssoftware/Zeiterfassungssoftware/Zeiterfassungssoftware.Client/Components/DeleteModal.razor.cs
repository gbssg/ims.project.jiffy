using Microsoft.AspNetCore.Components;
using static Zeiterfassungssoftware.Client.Components.DeleteModal;

namespace Zeiterfassungssoftware.Client.Components
{
    public partial class DeleteModal
    {
        public delegate void OnConfirmClick();
        public delegate void OnCancelClick();

        [Parameter]
        public EventCallback<OnConfirmClick> OnConfirm { get; set; }

        [Parameter]
        public EventCallback<OnCancelClick> OnCancel { get; set; }

        private void OnConfirmClicked()
        {
            OnConfirm.InvokeAsync();
        }

        private void OnCancelClicked()
        {
            OnCancel.InvokeAsync();
        }
    }
}