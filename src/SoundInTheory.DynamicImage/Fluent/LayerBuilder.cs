namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class LayerBuilder
	{
		public static ImageLayerBuilder Image
		{
			get { return new ImageLayerBuilder(); }
		}

		public static JuliaFractalLayerBuilder JuliaFractal
		{
			get { return new JuliaFractalLayerBuilder(); }
		}

		public static MandelbrotFractalLayerBuilder MandelbrotFractal
		{
			get { return new MandelbrotFractalLayerBuilder(); }
		}

		public static TextLayerBuilder Text
		{
			get { return new TextLayerBuilder(); }
		}

		public static PolygonShapeLayerBuilder PolygonShape
		{
			get { return new PolygonShapeLayerBuilder(); }
		}

		public static RectangleShapeLayerBuilder RectangleShape
		{
			get { return new RectangleShapeLayerBuilder(); }
		}

		public static VideoLayerBuilder Video
		{
			get { return new VideoLayerBuilder(); }
		}

		public abstract Layer ToLayer();
	}
}