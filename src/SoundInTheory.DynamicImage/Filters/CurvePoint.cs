using System.ComponentModel;

namespace SoundInTheory.DynamicImage.Filters
{
	public class CurvePoint : DirtyTrackingObject
	{
		/// <summary>
		/// Gets or sets the input value (between 0 and 255).
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the input value (between 0 and 255).")]
		public int Input
		{
			get { return (int)(PropertyStore["Input"] ?? 0); }
			set { PropertyStore["Input"] = value; }
		}

		/// <summary>
		/// Gets or sets the output value (between 0 and 255).
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the output value (between 0 and 255).")]
		public int Output
		{
			get { return (int)(PropertyStore["Output"] ?? 0); }
			set { PropertyStore["Output"] = value; }
		}
	}
}