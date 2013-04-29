namespace SoundInTheory.DynamicImage
{
	public class Point3DCollection : DirtyTrackingCollection<Point3D>
	{
		public Point3DCollection()
		{
			
		}

		public Point3DCollection(Point3D[] points)
			: base(points)
		{
			
		}
	}
}