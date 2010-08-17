using System;
using System.Web.UI.Design;
using System.Web.UI;
using System.IO;

namespace SoundInTheory.DynamicImage.Design
{
	public class DynamicImageDesigner : ControlDesigner
	{
		private DynamicImage _dynamicImage;

		public override void Initialize(System.ComponentModel.IComponent component)
		{
			_dynamicImage = (DynamicImage) component;
			_dynamicImage.Composition.DesignMode = true;
			_dynamicImage.Composition.Site = component.Site;
			base.Initialize(component);
		}

		public override string GetDesignTimeHtml()
		{
			DynamicImage control = (DynamicImage) Component;
			StringWriter textWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(textWriter);
			control.RenderControl(writer);

			CompositionImage image = control.Composition.GetCompositionImage();

			string designTimeImageUrl = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + "." + image.Properties.FileExtension);

			using (FileStream fileStream = File.OpenWrite(designTimeImageUrl))
			{
				image.Properties.GetEncoder().Save(fileStream);
			}
			string html = textWriter.ToString().Replace("src=\"", "src=\"file:///").Replace("src='", "src='file:///");

			writer.Close();
			textWriter.Close();

			return html;
		}
	}
}
