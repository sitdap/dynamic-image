using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Layers;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class TextLayerBuilder : BaseLayerBuilder<TextLayer, TextLayerBuilder>
	{
		public new TextLayerBuilder Text(string text)
		{
			Layer.Text = text;
			return this;
		}

		public TextLayerBuilder Width(int width)
		{
			Layer.Width = width;
			return this;
		}

		public TextLayerBuilder Height(int height)
		{
			Layer.Height = height;
			return this;
		}

		public TextLayerBuilder FontName(string name)
		{
			Layer.Font.Name = name;
			return this;
		}

		public TextLayerBuilder FontSize(float size)
		{
			Layer.Font.Size = size;
			return this;
		}

		public TextLayerBuilder HorizontalTextAlignment(TextAlignment alignment)
		{
			Layer.HorizontalTextAlignment = alignment;
			return this;
		}

		public TextLayerBuilder VerticalTextAlignment(VerticalAlignment alignment)
		{
			Layer.VerticalTextAlignment = alignment;
			return this;
		}

		public TextLayerBuilder StrokeColor(Color color)
		{
			Layer.StrokeColour = color;
			return this;
		}

		public TextLayerBuilder StrokeWidth(int width)
		{
			Layer.StrokeWidth = width;
			return this;
		}
	}
}