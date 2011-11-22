namespace SoundInTheory.DynamicImage.Filters
{
	public class CurvePoint : DirtyTrackingObject
	{
		/// <summary>
		/// Gets or sets the input value (between 0 and 255).
		/// </summary>
		public int Input
		{
			get { return (int)(this["Input"] ?? 0); }
			set { this["Input"] = value; }
		}

		/// <summary>
		/// Gets or sets the output value (between 0 and 255).
		/// </summary>
		public int Output
		{
			get { return (int)(this["Output"] ?? 0); }
			set { this["Output"] = value; }
		}
	}
}