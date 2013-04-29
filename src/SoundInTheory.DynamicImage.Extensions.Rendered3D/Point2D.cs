namespace SoundInTheory.DynamicImage
{
	public class Point2D : DirtyTrackingObject
	{
		public static Point2D Zero
		{
			get { return new Point2D(0, 0); }
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

		public Point2D(float x, float y)
		{
			X = x;
			Y = y;
		}
		public override string ToString()
		{
			return string.Format("{{X:{0} Y:{1}}}", X, Y);
		}
	}
}