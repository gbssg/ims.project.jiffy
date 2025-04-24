using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassungssoftware.SharedData.Activities
{
	public class ActivityTitle
	{
		public Guid Id { get; set; }
		public string Value { get; set; } = string.Empty;
        public bool Favorite { get; set; }
		 

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
		public override string ToString() => Id.ToString();
		
    }

	public class ActivityDescription
	{
        public Guid Id { get; set; }
		public string Value { get; set; } = string.Empty;
        public bool Favorite { get; set; }

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
        public override string ToString() => Id.ToString();
    }
}
