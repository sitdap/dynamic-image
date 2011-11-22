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
			get { return (int)(ViewState["Input"] ?? 0); }
			set { ViewState["Input"] = value; }
		}

		/// <summary>
		/// Gets or sets the output value (between 0 and 255).
		/// </summary>
		[DefaultValue(0), Description("Gets or sets the output value (between 0 and 255).")]
		public int Output
		{
			get { return (int)(ViewState["Output"] ?? 0); }
			set { ViewState["Output"] = value; }
		}
	}
}