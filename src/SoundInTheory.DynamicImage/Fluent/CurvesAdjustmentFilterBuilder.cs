using SoundInTheory.DynamicImage.Filters;

namespace SoundInTheory.DynamicImage.Fluent
{
	public class CurvesAdjustmentFilterBuilder : BaseFilterBuilder<CurvesAdjustmentFilter, CurvesAdjustmentFilterBuilder>
	{
		public CurvesAdjustmentFilterBuilder UsingFile(string fileName)
		{
			Filter.PhotoshopCurvesFileName = fileName;
			return this;
		}

		public CurvesAdjustmentFilterBuilder UsingCurves(CurveCollection curves)
		{
			Filter.Curves = curves;
			return this;
		}
	}
}