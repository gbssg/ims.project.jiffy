using Microsoft.AspNetCore.Components;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
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
			/*
			this.messages.Add("Test");
			this.messages.Add("Test1");
		*/
		}
		private void SendMessage()
		{
			this.messages.Add(this.currentMessage);
			this.currentMessage = String.Empty;
		}

		private void startClock()
		{
			this.currentMessage = "Hoi Wält!";
		}

		public string buttonIndex = "Start";
		bool buttonStatus = false;

		private void changeButtonIndex()
		{
			
			if(buttonStatus == false)
			{
				buttonStatus = true;
				buttonIndex = "Stop";
			}
			else
			{
				buttonStatus = false;
				buttonIndex = "Start";
			}
			
		}
	}
	


}