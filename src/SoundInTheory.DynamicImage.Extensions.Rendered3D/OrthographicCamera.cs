using Meshellator;
using Nexus.Graphics;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Represents an orthographic projection camera. 
	/// </summary>
	/// <remarks>
	/// This class specifies an orthogonal projection of a 3-D model to a 2-D visual surface. Like PerspectiveCamera, it specifies a position, 
	/// viewing direction, and "upward" direction. Unlike PerspectiveCamera, however, OrthographicCamera describes a projection that does 
	/// not include perspective foreshortening. In other words, OrthographicCamera describes a viewing box whose sides are parallel, 
	/// instead of one whose sides meet in a point at the scene's horizon. 
	/// </remarks>
	public class OrthographicCamera : ProjectionCamera
	{
		/// <summary>
		/// Gets or sets the width of the camera's viewing box, in world units.
		/// </summary>
		/// <remarks>
		/// Because the OrthographicCamera describes a projection that does not include perspective foreshortening, its viewing box 
		/// has parallel sides. The width of the viewing box can therefore be specified with a single value.
		/// </remarks>
		public float Width
		{
			get { return (float)(this["Width"] ?? 2.0f); }
			set { this["Width"] = value; }
		}

		public override Nexus.Graphics.Cameras.Camera GetNexusCamera(Scene scene, Viewport viewport)
		{
			return new Nexus.Graphics.Cameras.OrthographicCamera
			{
				FarPlaneDistance = FarPlaneDistance,
				LookDirection = new Nexus.Vector3D(LookDirection.X, LookDirection.Y, LookDirection.Z),
				NearPlaneDistance = NearPlaneDistance,
				Position = new Nexus.Point3D(Position.X, Position.Y, Position.Z),
				UpDirection = new Nexus.Vector3D(UpDirection.X, UpDirection.Y, UpDirection.Z),
				Width = Width
			};
		}
	}
}