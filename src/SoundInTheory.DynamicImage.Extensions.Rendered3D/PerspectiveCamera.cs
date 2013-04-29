using Meshellator;
using Nexus;
using Nexus.Graphics;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Represents a perspective projection camera. 
	/// </summary>
	/// <remarks>
	/// PerspectiveCamera specifies a projection of a 3-D model to a 2-D visual surface. This projection includes perspective foreshortening. 
	/// In other words, the PerspectiveCamera describes a frustrum whose sides converge toward a point on the horizon. Objects closer to 
	/// the camera appear larger, and objects farther from the camera appear smaller.
	/// </remarks>
	public class PerspectiveCamera : ProjectionCamera
	{
		/// <summary>
		/// Gets or sets a value that represents the camera's horizontal field of view in radians. 
		/// </summary>
		public float FieldOfView
		{
			get { return (float)(this["FieldOfView"] ?? MathUtility.PI_OVER_4); }
			set { this["FieldOfView"] = value; }
		}

		public override Nexus.Graphics.Cameras.Camera GetNexusCamera(Scene scene, Viewport viewport)
		{
			return new Nexus.Graphics.Cameras.PerspectiveCamera
			{
				FarPlaneDistance = FarPlaneDistance,
				FieldOfView = FieldOfView,
				LookDirection = new Nexus.Vector3D(LookDirection.X, LookDirection.Y, LookDirection.Z),
				NearPlaneDistance = NearPlaneDistance,
				Position = new Nexus.Point3D(Position.X, Position.Y, Position.Z),
				UpDirection = new Nexus.Vector3D(UpDirection.X, UpDirection.Y, UpDirection.Z)
			};
		}
	}
}