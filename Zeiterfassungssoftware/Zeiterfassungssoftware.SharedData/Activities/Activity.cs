using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Activities
{
	public class ActivityTitle
	{
		public string Value { get; set; } = string.Empty;
		
		public ActivityTitle(string Title)
		{
			Value = Title;
		}

        public override bool Equals(object? obj)
        {
            ActivityTitle? Other = obj as ActivityTitle;

			if(Other is null)
				return false;

			string Value = Normalize(this.Value);
			string OtherValue = Normalize(Other.Value);

			return Value.Equals(OtherValue);
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

	public class ActivityDescription
	{
		public string Value { get; set; } = string.Empty;

		public ActivityDescription(string Description)
		{
			Value = Description;
		}

        public override bool Equals(object? obj)
        {
            ActivityDescription? Other = obj as ActivityDescription;

            if (Other is null)
                return false;

            string Value = Normalize(this.Value);
            string OtherValue = Normalize(Other.Value);

            return Value.Equals(OtherValue);
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
