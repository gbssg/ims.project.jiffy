using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Zeiterfassungssoftware.Client.Services;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.ShouldTimes;
using Zeiterfassungssoftware.SharedData.Times;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Client
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();


            builder.Services.AddSingleton<ITimeEntryProvider, RemoteTimeEntryProvider>();
            builder.Services.AddSingleton<IActivityProvider, RemoteActivityProvider>();
            builder.Services.AddSingleton<IClassProvider, RemoteClassProvider>();
            builder.Services.AddSingleton<IUserProvider, RemoteUserProvider>();
            builder.Services.AddSingleton<IShouldTimeProvider, RemoteShouldTimeProvider>();

            await builder.Build().RunAsync();
        }
    }
}
