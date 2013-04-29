using System.Windows.Media;
using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class RenderedLayerBuilder : BaseLayerBuilder<RenderedLayer, RenderedLayerBuilder>
	{
        public RenderedLayerBuilder Colour(Color colour)
        {
            Layer.BackgroundColour = colour;
            return this;
        }

		public RenderedLayerBuilder Width(int width)
		{
			Layer.Width = width;
			return this;
		}

		public RenderedLayerBuilder Height(int height)
		{
			Layer.Height = height;
			return this;
		}

		public RenderedLayerBuilder SourceFileName(string fileName)
		{
			Layer.SourceFileName = fileName;
			return this;
		}

		public RenderedLayerBuilder SourceScene(InlineSceneSource source)
		{
			Layer.Source = source;
			return this;
		}

		public RenderedLayerBuilder Lighting(bool enabled)
		{
			Layer.LightingEnabled = enabled;
			return this;
		}

		public RenderedLayerBuilder ReverseWindingOrder()
		{
			Layer.ReverseWindingOrder = true;
			return this;
		}

		public RenderedLayerBuilder WithAutoCamera(int yaw, int pitch)
		{
			Layer.Camera = new AutoCamera { Yaw = yaw, Pitch = pitch };
			return this;
		}
	}
}