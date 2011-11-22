using System.Windows.Media;
using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class ClosedShapeLayerBuilder<TLayer, TLayerBuilder> : ShapeLayerBuilder<TLayer, TLayerBuilder>
		where TLayer : ClosedShapeLayer, new()
		where TLayerBuilder : ClosedShapeLayerBuilder<TLayer, TLayerBuilder>
	{
		public TLayerBuilder Fill(string backgroundColorHexRef)
		{
			Layer.Fill.BackgroundColour = (Color) ColorConverter.ConvertFromString(backgroundColorHexRef);
			return (TLayerBuilder) this;
		}
	}
}