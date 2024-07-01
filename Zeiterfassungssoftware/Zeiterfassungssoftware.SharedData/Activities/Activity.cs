using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Activities
{
	public class ActivityTitle(string Title)
	{
		public string Value { get; set; } = Title;

		public override bool Equals(object? obj)
        {
            if(obj is ActivityDescription Other)
			{
				string Value = Normalize(this.Value);
				string OtherValue = Normalize(Other.Value);

				return Value.Equals(OtherValue);
			}

			return false;
        }

		public override int GetHashCode()
		{
			return Normalize(Value).GetHashCode();
		}

		private string Normalize(string Input)
		{
			return Input.ToLower().Trim();
		}
    }

	public class ActivityDescription(string Description)
	{
		public string Value { get; set; } = Description;

		public override bool Equals(object? obj)
		{
			if (obj is ActivityDescription Other)
			{ 
				string Value = Normalize(this.Value);
				string OtherValue = Normalize(Other.Value);

				return Value.Equals(OtherValue);
			}
			return false;
        }

        public override int GetHashCode()
        {
            return Normalize(Value).GetHashCode();
        }

        private string Normalize(string Input)
        {
            return Input.ToLower().Trim();
        }
    }
}
