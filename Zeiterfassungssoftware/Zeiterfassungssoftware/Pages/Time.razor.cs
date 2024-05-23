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
	}
}