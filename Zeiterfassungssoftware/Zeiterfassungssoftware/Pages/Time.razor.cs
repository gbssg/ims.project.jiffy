using Microsoft.AspNetCore.Components;
using Zeiterfassungssoftware.Data.Activities;


namespace Zeiterfassungssoftware.Pages
{

	public partial class Time
	{
		private List<ActivityTitle> ActivityTitles;
		private List<ActivityDescription> ActivityDescription;
		protected override async Task OnInitializedAsync()
		{
			ActivityProvider provider = new ActivityProvider();
			ActivityTitles = provider.LoadSavedTitles();
			ActivityDescription = provider.LoadSavedDescriptions();
		}

		List<string> messages = new();

		string currentMessage;
		protected override void OnInitialized()
		{
			this.messages.Add("Test");
			this.messages.Add("Test1");
		}
		private void SendMessage()
		{
			this.messages.Add(this.currentMessage);
			this.currentMessage = String.Empty;
		}
	}
	


}