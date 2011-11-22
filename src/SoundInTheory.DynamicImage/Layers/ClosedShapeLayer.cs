namespace SoundInTheory.DynamicImage.Layers
{
	/// <summary>
	/// An abstract base class that provides basic functionality for derived close shape classes.
	/// </summary>
	public abstract class ClosedShapeLayer : ShapeLayer
	{
		public Fill Fill
		{
			get { return (Fill)(this["Fill"] ?? (this["Fill"] = new Fill())); }
			set { this["Fill"] = value; }
		}
	}
}
