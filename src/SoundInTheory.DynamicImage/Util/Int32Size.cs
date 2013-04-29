namespace SoundInTheory.DynamicImage.Util
{
	public struct Int32Size
	{
		public static Int32Size Empty
		{
			get { return new Int32Size(); }
		}

		public int Width { get; set; }
		public int Height { get; set; }

		public bool IsEmpty
		{
			get { return Width == 0 && Height == 0; }
		}

		public Int32Size(int width, int height)
			: this()
		{
			Width = width;
			Height = height;
		}
	}
}