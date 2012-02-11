using System;
using System.Web;
using System.Web.Mvc;
using SoundInTheory.DynamicImage.Fluent;

namespace SoundInTheory.DynamicImage.Mvc
{
	public static class HtmlHelperExtensions
	{
		public static HtmlString DynamicImageTag(this HtmlHelper html, Action<CompositionBuilder> callback)
		{
			var tagBuilder = new TagBuilder("img");

			var compositionBuilder = new CompositionBuilder();
			callback(compositionBuilder);
			tagBuilder.Attributes["src"] = compositionBuilder.Url;

			return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
		}
	}
}
