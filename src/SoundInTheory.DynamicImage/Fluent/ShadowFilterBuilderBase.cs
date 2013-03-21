using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class ShadowFilterBuilderBase<TFilter, TFilterBuilder> : BaseFilterBuilder<TFilter, TFilterBuilder>
		where TFilter : ShadowFilterBase, new()
		where TFilterBuilder : ShadowFilterBuilderBase<TFilter, TFilterBuilder>
	{
		public TFilterBuilder Color(Color color)
		{
			Filter.Color = color;
			return (TFilterBuilder) this;
		}

		public TFilterBuilder Opacity(int opacity)
		{
			Filter.Opacity = opacity;
			return (TFilterBuilder) this;
		}

		public TFilterBuilder Size(int size)
		{
			Filter.Size = size;
			return (TFilterBuilder)this;
		}
	}
}