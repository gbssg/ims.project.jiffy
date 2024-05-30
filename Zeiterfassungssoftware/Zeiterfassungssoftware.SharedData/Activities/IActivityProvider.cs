using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Activities
{
	public interface IActivityProvider
	{
		public bool IsLoaded { get; }

		public void Remove(ActivityDescription ActivityDescription);
		public void Remove(ActivityTitle ActivityTitle);

		public void Add(ActivityDescription ActivityDescription);
		public void Add(ActivityTitle ActivityTitle);

		public List<ActivityDescription> GetActivityDescription();
		public List<ActivityTitle> GetActivityTitles();
	}
}
