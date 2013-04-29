namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// An abstract base class for perspective and orthographic projection cameras. 
	/// </summary>
	public abstract class ProjectionCamera : Camera
	{
		/// <summary>
		/// Gets or sets a value that specifies the distance from the camera of the camera's far clip plane.
		/// </summary>
		public float FarPlaneDistance
		{
			get { return (float)(this["FarPlaneDistance"] ?? 10000.0f); }
			set { this["FarPlaneDistance"] = value; }
		}

		/// <summary>
		/// Gets or sets a Vector3D which defines the direction in which the camera is looking in world coordinates.
		/// </summary>
		public Vector3D LookDirection
		{
			get { return (Vector3D)(this["LookDirection"] ?? Vector3D.Forward); }
			set { this["LookDirection"] = value; }
		}

		/// <summary>
		/// Gets or sets a value that specifies the distance from the camera of the camera's near clip plane.
		/// </summary>
		public float NearPlaneDistance
		{
			get { return (float)(this["NearPlaneDistance"] ?? 0.125f); }
			set { this["NearPlaneDistance"] = value; }
		}

		/// <summary>
		/// Gets or sets the position of the camera in world coordinates.
		/// </summary>
		public Point3D Position
		{
			get { return (Point3D)(this["Position"] ?? Point3D.Zero); }
			set { this["Position"] = value; }
		}

		/// <summary>
		/// Gets or sets a Vector3D which defines the upward direction of the camera.
		/// </summary>
		public Vector3D UpDirection
		{
			get { return (Vector3D)(this["UpDirection"] ?? Vector3D.Up); }
			set { this["UpDirection"] = value; }
		}
	}
}