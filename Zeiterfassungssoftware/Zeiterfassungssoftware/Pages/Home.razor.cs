using Microsoft.JSInterop;
using Zeiterfassungssoftware.Data.Filter;
using Zeiterfassungssoftware.Services;

namespace Zeiterfassungssoftware.Pages
{
    public partial class Home
    {
        public IFilter Filter = new StringFilter("Test123");
        protected override async Task OnInitializedAsync()
        {
            
        }

        public void PopUp()
        {
            JS.InvokeVoidAsync("alert", "W\u2087");
        }
    }
}