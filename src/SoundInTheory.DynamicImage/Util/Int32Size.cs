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

		public Int32Size(int width, int height)
			: this()
		{
			Width = width;
			Height = height;
		}
	}
}