namespace SoundInTheory.DynamicImage
{
	public class Padding : DirtyTrackingObject
	{
		public int Top
		{
			get { return (int) (this["Top"] ?? 0); }
			set { this["Top"] = value; }
		}

		public int Left
		{
			get { return (int)(this["Left"] ?? 0); }
			set { this["Left"] = value; }
		}

		public int Bottom
		{
			get { return (int)(this["Bottom"] ?? 0); }
			set { this["Bottom"] = value; }
		}

		public int Right
		{
			get { return (int)(this["Right"] ?? 0); }
			set { this["Right"] = value; }
		}
	}
}
