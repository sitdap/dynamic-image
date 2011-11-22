namespace SoundInTheory.DynamicImage.Filters
{
	public class Curve : DirtyTrackingObject
	{
		public CurvePointCollection Points
		{
			get { return (CurvePointCollection)(PropertyStore["Points"] ?? (PropertyStore["Points"] = new CurvePointCollection())); }
			set { PropertyStore["Points"] = value; }
		}
	}
}