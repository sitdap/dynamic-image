namespace SoundInTheory.DynamicImage
{
	public class Vector3D : DirtyTrackingObject
	{
		public static Vector3D Forward
		{
			get { return new Vector3D(0, 0, -1); }
		}

		public static Vector3D Up
		{
			get { return new Vector3D(0, 1, 0); }
		}

		public float X
		{
			get { return (float)(this["X"] ?? 0.0f); }
			set { this["X"] = value; }
		}

		public float Y
		{
			get { return (float)(this["Y"] ?? 0.0f); }
			set { this["Y"] = value; }
		}

		public float Z
		{
			get { return (float)(this["Z"] ?? 0.0f); }
			set { this["Z"] = value; }
		}

		public Vector3D(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1} Z:{2}}}", X, Y, Z);
		}
	}
}