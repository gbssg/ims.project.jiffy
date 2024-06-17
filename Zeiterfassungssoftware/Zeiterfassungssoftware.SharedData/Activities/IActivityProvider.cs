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

		public bool Contains(ActivityDescription ActivityDescription);
        public bool Contains(ActivityTitle ActivityTitle);

        public List<ActivityDescription> GetActivityDescriptions();
		public List<ActivityTitle> GetActivityTitles();
	}
}
