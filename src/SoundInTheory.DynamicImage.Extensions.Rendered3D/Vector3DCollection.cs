namespace SoundInTheory.DynamicImage
{
	public class Vector3DCollection : DirtyTrackingCollection<Vector3D>
	{
		public Vector3DCollection()
		{
			
		}

		public Vector3DCollection(Vector3D[] points)
			: base(points)
		{
			
		}
	}
}