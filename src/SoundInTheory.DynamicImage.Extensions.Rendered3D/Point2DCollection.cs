namespace SoundInTheory.DynamicImage
{
	public class Point2DCollection : DirtyTrackingCollection<Point2D>
	{
		public Point2DCollection()
		{
			
		}

		public Point2DCollection(Point2D[] points)
			: base(points)
		{
			
		}
	}
}