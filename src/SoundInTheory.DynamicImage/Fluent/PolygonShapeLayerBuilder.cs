using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class PolygonShapeLayerBuilder : ClosedShapeLayerBuilder<PolygonShapeLayer, PolygonShapeLayerBuilder>
	{
		public PolygonShapeLayerBuilder Sides(int sides)
		{
			Layer.Sides = sides;
			return this;
		}
	}
}