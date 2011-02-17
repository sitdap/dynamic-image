namespace SoundInTheory.DynamicImage.Filters
{
	public class SolarizeFilter : TransferFilter
	{
		protected override float GetTransferFunctionValue(float value)
		{
			return (value > 0.5f) ? 2 * (value - 0.5f) : 2 * (0.5f - value);
		}

		public override string ToString()
		{
			return "Solarize";
		}
	}
}
