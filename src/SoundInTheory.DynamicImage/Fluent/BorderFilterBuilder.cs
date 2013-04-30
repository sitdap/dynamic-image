using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class BorderFilterBuilder : BaseFilterBuilder<BorderFilter, BorderFilterBuilder>
	{
		public BorderFilterBuilder Width(int width)
		{
			Filter.Width = width;
			return this;
		}

		public BorderFilterBuilder Fill(string backgroundColorHexRef)
		{
			Filter.Fill.BackgroundColor = Color.FromHtml(backgroundColorHexRef);
			return this;
		}

		public BorderFilterBuilder Fill(Color color)
		{
			Filter.Fill.BackgroundColor = color;
			return this;
		}
	}
}