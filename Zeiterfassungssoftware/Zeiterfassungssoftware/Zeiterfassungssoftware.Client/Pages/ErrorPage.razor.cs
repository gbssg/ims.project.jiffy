using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class ErrorPage
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string? Message { get; set; }
        [Parameter]
        public string? Url { get; set; }

        protected override void OnInitialized()
        {
            var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
            Dictionary<string, StringValues> Query = QueryHelpers.ParseQuery(uri.Query);

            if (Query.TryGetValue("msg", out var Message))
            {
                this.Message = Message;
            }
            if (Query.TryGetValue("returnUrl", out var Url))
            {
                this.Url = Url;
            }
        }
    }
}