using System.Configuration;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Configuration;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class DynamicImageBuilder
	{
		private readonly DynamicImage _dynamicImage;

		public DynamicImageBuilder()
		{
			_dynamicImage = new DynamicImage();

			DynamicImageSection config = (DynamicImageSection) ConfigurationManager.GetSection("soundInTheory/dynamicImage");
			if (config != null)
				_dynamicImage.ImageFormat = config.DefaultImageFormat;
		}

		public string Url
		{
			get { return _dynamicImage.ImageUrl; }
		}

		public DynamicImageBuilder Width(int width)
		{
			_dynamicImage.OutputWidth = width;
			_dynamicImage.AutoSize = false;
			return this;
		}

		public DynamicImageBuilder Height(int height)
		{
			_dynamicImage.OutputHeight = height;
			_dynamicImage.AutoSize = false;
			return this;
		}

		public DynamicImageBuilder ImageFormat(DynamicImageFormat imageFormat)
		{
			_dynamicImage.ImageFormat = imageFormat;
			return this;
		}

		public DynamicImageBuilder FillBackgroundColor(Color backgroundColor)
		{
			_dynamicImage.Fill.BackgroundColour = backgroundColor;
			return this;
		}

		public DynamicImageBuilder WithLayer(LayerBuilder layerBuilder)
		{
			_dynamicImage.Layers.Add(layerBuilder.ToLayer());
			return this;
		}
	}
}