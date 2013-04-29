namespace SoundInTheory.DynamicImage
{
	public class Point3D : DirtyTrackingObject
	{
		public static Point3D Zero
		{
			get { return new Point3D(0, 0, 0); }
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

		public Point3D(float x, float y, float z)
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