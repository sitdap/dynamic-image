namespace SoundInTheory.DynamicImage.Filters
{
	public class Curve : DirtyTrackingObject
	{
		public CurvePointCollection Points
		{
			get { return (CurvePointCollection)(this["Points"] ?? (this["Points"] = new CurvePointCollection())); }
			set { this["Points"] = value; }
		}
	}
}