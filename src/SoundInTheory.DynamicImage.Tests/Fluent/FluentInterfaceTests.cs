using SoundInTheory.DynamicImage.Fluent;
using NUnit.Framework;

namespace SoundInTheory.DynamicImage.Tests.Fluent
{
	//[TestFixture]
	public class FluentInterfaceTests
	{
		//[Test]
		public void CanCreateImageLayer()
		{
			string imageUrl = new CompositionBuilder()
				.WithLayer(LayerBuilder.Image.SourceFile("myimage.png")
					.WithFilter(FilterBuilder.Resize.ToWidth(800))
				)
				.WithLayer(LayerBuilder.Text.Text("Hello World")
					.WithFilter(FilterBuilder.OuterGlow)
				).Url;

			/*
			 * <sitdap:DynamicImage runat="server" ImageFormat="Jpeg">
				<Layers>
					<sitdap:ImageLayer SourceFileName="~/Assets/Images/AutumnLeaves.jpg">
						<Filters>
							<sitdap:ResizeFilter Mode="Uniform" Height="200" />
						</Filters>
					</sitdap:ImageLayer>
				</Layers>
			</sitdap:DynamicImage>
			 * */
		}
	}
}