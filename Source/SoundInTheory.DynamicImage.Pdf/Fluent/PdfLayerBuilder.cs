namespace SoundInTheory.DynamicImage.Fluent
{
	public class PdfLayerBuilder : BaseLayerBuilder<PdfLayer, PdfLayerBuilder>
	{
		public PdfLayerBuilder SourceFileName(string fileName)
		{
			Layer.SourceFileName = fileName;
			return this;
		}

		public PdfLayerBuilder PageNumber(int pageNumber)
		{
			Layer.PageNumber = pageNumber;
			return this;
		}
	}
}