namespace SoundInTheory.DynamicImage
{
	public class Mesh : DirtyTrackingObject
	{
		public Point3DCollection Positions
		{
			get { return (Point3DCollection)this["Positions"]; }
			set { this["Positions"] = value; }
		}

		public Vector3DCollection Normals
		{
			get { return (Vector3DCollection)this["Normals"]; }
			set { this["Normals"] = value; }
		}

		public Point2DCollection TextureCoordinates
		{
			get { return (Point2DCollection)this["TextureCoordinates"]; }
			set { this["TextureCoordinates"] = value; }
		}

		public IndexCollection Indices
		{
			get { return (IndexCollection)this["Indices"]; }
			set { this["Indices"] = value; }
		}

		public Material Material
		{
			get { return (Material)this["Material"]; }
			set { this["Material"] = value; }
		}

		public Mesh()
		{
			Positions = new Point3DCollection();
			Normals = new Vector3DCollection();
			TextureCoordinates = new Point2DCollection();
			Indices = new IndexCollection();
			Material = new Material();
		}
	}
}