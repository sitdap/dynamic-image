using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class ClosedShapeLayerBuilder<TLayer, TLayerBuilder> : ShapeLayerBuilder<TLayer, TLayerBuilder>
		where TLayer : ClosedShapeLayer, new()
		where TLayerBuilder : ClosedShapeLayerBuilder<TLayer, TLayerBuilder>
	{
		public TLayerBuilder Fill(string backgroundColorHexRef)
		{
			Layer.Fill.BackgroundColor = Color.FromHtml(backgroundColorHexRef);
			return (TLayerBuilder) this;
		}

		public TLayerBuilder Fill(Color color)
		{
			Layer.Fill.BackgroundColor = color;
			return (TLayerBuilder)this;
		}
	}
}