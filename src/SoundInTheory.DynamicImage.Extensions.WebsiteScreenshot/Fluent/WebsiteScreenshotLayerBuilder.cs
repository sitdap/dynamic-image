using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class WebsiteScreenshotLayerBuilder : BaseLayerBuilder<WebsiteScreenshotLayer, WebsiteScreenshotLayerBuilder>
	{
		public WebsiteScreenshotLayerBuilder Timeout(int timeout)
		{
			Layer.Timeout = timeout;
			return this;
		}

		public WebsiteScreenshotLayerBuilder WebsiteUrl(string url)
		{
			Layer.WebsiteUrl = url;
			return this;
		}

		public WebsiteScreenshotLayerBuilder BrowserWidth(int width)
		{
			Layer.BrowserWidth = width;
			return this;
		}
	}
}