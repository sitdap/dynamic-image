namespace SoundInTheory.DynamicImage.Fluent
{
	public class WebsiteScreenshotLayerBuilder : BaseLayerBuilder<WebsiteScreenshotLayer, WebsiteScreenshotLayerBuilder>
	{
		public WebsiteScreenshotLayerBuilder WebsiteUrl(string url)
		{
			Layer.WebsiteUrl = url;
			return this;
		}
	}
}