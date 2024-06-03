using Microsoft.AspNetCore.Components;

namespace Zeiterfassungssoftware.Pages
{
	public partial class Weather
	{
		public string message = string.Empty;	
		public void OnClick(ChangeEventArgs args)
		{
			message = args.Value.ToString();
		}
	}
}