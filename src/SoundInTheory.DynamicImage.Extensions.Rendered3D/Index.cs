namespace SoundInTheory.DynamicImage
{
	public class Index : DirtyTrackingObject
	{
		public int Value
		{
			get { return (int)(this["Value"] ?? 0); }
			set { this["Value"] = value; }
		}

		public Index(int value)
		{
			Value = value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public static implicit operator Index(int value)
		{
			return new Index(value);
		}
	}
}