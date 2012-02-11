using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage
{
	public class FontDescription
	{
		public Typeface Typeface { get; private set; }
		public TextDecorationCollection TextDecorations { get; private set; }
		public double Size { get; private set; }
		
		public FontDescription(Typeface typeface, TextDecorationCollection textDecorations, double size)
		{
			Typeface = typeface;
			TextDecorations = textDecorations;
			Size = size;
		}
	}
}