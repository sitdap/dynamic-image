namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class LayerBuilder
	{
		public static ImageLayerBuilder Image
		{
			get { return new ImageLayerBuilder(); }
		}

		public static TextLayerBuilder Text
		{
			get { return new TextLayerBuilder(); }
		}

		public static RectangleShapeLayerBuilder RectangleShape
		{
			get { return new RectangleShapeLayerBuilder(); }
		}

		public abstract Layer ToLayer();
	}
}