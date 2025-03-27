using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.SharedData.Activities
{
	public interface IActivityProvider
	{
		public bool IsLoaded { get; }

		public void Remove(object Obj);
		public void Add(object Obj);
		public bool Contains(object Obj);
        public Task<object> Update(object Obj);

        public List<ActivityDescription> GetActivityDescriptions();
		public List<ActivityTitle> GetActivityTitles();
	}
}
