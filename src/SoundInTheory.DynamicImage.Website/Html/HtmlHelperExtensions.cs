using System.Web.Mvc;
using SoundInTheory.DynamicImage.Fluent;

namespace SoundInTheory.DynamicImage.Website.Html
{
	public static class HtmlHelperExtensions
	{
		public static MvcHtmlString ResizedImageTag(this HtmlHelper htmlHelper, string imageHref, int width)
		{
			var tagBuilder = new TagBuilder("img");
			tagBuilder.Attributes["src"] = new CompositionBuilder()
				.WithLayer(
					LayerBuilder.Image.SourceFile(imageHref).WithFilter(
						FilterBuilder.Resize.ToWidth(width)
						)
				).Url;
			return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
		}
	}
}