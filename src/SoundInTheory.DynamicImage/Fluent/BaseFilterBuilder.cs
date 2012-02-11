using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public abstract class BaseFilterBuilder<TFilter, TFilterBuilder> : FilterBuilder
		where TFilter : Filter, new()
		where TFilterBuilder : BaseFilterBuilder<TFilter, TFilterBuilder>
	{
		protected readonly TFilter _filter;

		protected BaseFilterBuilder()
		{
			_filter = new TFilter();
		}

		protected TFilter Filter
		{
			get { return _filter; }
		}

		public override Filter ToFilter()
		{
			return _filter;
		}
	}
}