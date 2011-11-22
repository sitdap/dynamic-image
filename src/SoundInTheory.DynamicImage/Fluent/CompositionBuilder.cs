using System.Configuration;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Configuration;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class CompositionBuilder
	{
		private readonly Composition _composition;

		public CompositionBuilder()
		{
			_composition = new Composition();

			DynamicImageSection config = (DynamicImageSection) ConfigurationManager.GetSection("soundInTheory/dynamicImage");
			if (config != null)
				_composition.ImageFormat = config.DefaultImageFormat;
		}

		public string Url
		{
			get { return ImageManager.GetImageProperties(_composition).Url; }
		}

		public CompositionBuilder Width(int width)
		{
			_composition.Width = width;
			_composition.AutoSize = false;
			return this;
		}

		public CompositionBuilder Height(int height)
		{
			_composition.Height = height;
			_composition.AutoSize = false;
			return this;
		}

		public CompositionBuilder ImageFormat(DynamicImageFormat imageFormat)
		{
			_composition.ImageFormat = imageFormat;
			return this;
		}

		public CompositionBuilder FillBackgroundColor(Color backgroundColor)
		{
			_composition.Fill.BackgroundColour = backgroundColor;
			return this;
		}

		public CompositionBuilder WithLayer(LayerBuilder layerBuilder)
		{
			_composition.Layers.Add(layerBuilder.ToLayer());
			return this;
		}

		public CompositionBuilder WithGlobalFilter(FilterBuilder filterBuilder)
		{
			_composition.Filters.Add(filterBuilder.ToFilter());
			return this;
		}
	}
}