using System.Windows.Media;
using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class ShapeLayerBuilder<TLayer, TLayerBuilder> : BaseLayerBuilder<TLayer, TLayerBuilder>
		where TLayer : ShapeLayer, new()
		where TLayerBuilder : ShapeLayerBuilder<TLayer, TLayerBuilder>
	{
		public TLayerBuilder Width(int width)
		{
			Layer.Width = width;
			return (TLayerBuilder) this;
		}

		public TLayerBuilder Height(int height)
		{
			Layer.Height = height;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder Roundness(int roundness)
		{
			Layer.Roundness = roundness;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder StrokeWidth(float width)
		{
			Layer.StrokeWidth = width;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder StrokeFill(string colorHexRef)
		{
			Layer.StrokeFill.BackgroundColour = (Color) ColorConverter.ConvertFromString(colorHexRef);
			return (TLayerBuilder)this;
		}

		public TLayerBuilder StrokeFill(Color color)
		{
			Layer.StrokeFill.BackgroundColour = color;
			return (TLayerBuilder)this;
		}
	}
}