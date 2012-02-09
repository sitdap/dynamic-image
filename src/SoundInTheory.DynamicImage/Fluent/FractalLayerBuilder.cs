using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class FractalLayerBuilder<TLayer, TLayerBuilder> : BaseLayerBuilder<TLayer, TLayerBuilder>
		where TLayer : FractalLayer, new()
		where TLayerBuilder : FractalLayerBuilder<TLayer, TLayerBuilder>
	{
		public TLayerBuilder Width(int width)
		{
			Layer.Width = width;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder Height(int height)
		{
			Layer.Height = height;
			return (TLayerBuilder)this;
		}

		public TLayerBuilder MaxIterations(int iterations)
		{
			Layer.MaxIterations = iterations;
			return (TLayerBuilder)this;
		}
	}
}