using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Activities
{
	public class ActivityTitle
	{
		public string Value { get; set; }

		public ActivityTitle(string Value)
		{
			this.Value = Value;
		}

		public override bool Equals(object? obj)
        {
            if(obj is ActivityTitle Other)
			{
				string Value = Normalize(this.Value);
				string OtherValue = Normalize(Other.Value);

				return Value.Equals(OtherValue);
			}

			return false;
        }

        public override int GetHashCode() => Normalize(Value).GetHashCode();
        private string Normalize(string Input) => Input.ToLower().Trim();
    }

	public class ActivityDescription
	{
		public string Value { get; set; }

		public ActivityDescription(string Value)
		{
			this.Value = Value;
		}

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

        public override int GetHashCode() => Normalize(Value).GetHashCode();
        private string Normalize(string Input) => Input.ToLower().Trim();
    }
}
