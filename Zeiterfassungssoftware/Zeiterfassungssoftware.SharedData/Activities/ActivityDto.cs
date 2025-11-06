namespace Zeiterfassungssoftware.SharedData.Activities
{
	public class ActivityTitleDto
	{
		public Guid Id { get; set; }
		public string Value { get; set; } = string.Empty;
        public bool Favorite { get; set; }
		 

        public override bool Equals(object? obj)
        {
            if(obj is ActivityTitleDto Other)
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

	public class ActivityDescriptionDto
	{
        public Guid Id { get; set; }
		public string Value { get; set; } = string.Empty;
        public bool Favorite { get; set; }

        public override bool Equals(object? obj)
		{
			if (obj is ActivityDescriptionDto Other)
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
