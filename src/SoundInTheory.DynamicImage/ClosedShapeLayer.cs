
namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// An abstract base class that provides basic functionality for derived close shape classes.
	/// </summary>
	public abstract class ClosedShapeLayer : ShapeLayer
	{
		public Fill Fill
		{
			get { return (Fill) (ViewState["Fill"] ?? (ViewState["Fill"] = new Fill())); }
			set { ViewState["Fill"] = value; }
		}
	}
}
