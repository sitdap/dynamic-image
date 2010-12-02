using System.ComponentModel;
using System.IO;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	public class ContentAwareResizeFilter : Filter
	{
		#region Properties

		/// <summary>
		/// Gets or sets the desired image width.
		/// </summary>
		[DefaultValue(typeof(Unit), "Empty"), Description("Gets or sets the desired image width.")]
		public Unit Width
		{
			get { return (Unit)(ViewState["Width"] ?? Unit.Empty); }
			set { ViewState["Width"] = value; }
		}

		/// <summary>
		/// Gets or sets the desired image height.
		/// </summary>
		[DefaultValue(typeof(Unit), "Empty"), Description("Gets or sets the desired image height.")]
		public Unit Height
		{
			get { return (Unit)(ViewState["Height"] ?? Unit.Empty); }
			set { ViewState["Height"] = value; }
		}

		/// <summary>
		/// Gets or sets the convolution type.
		/// </summary>
		[DefaultValue(ContentAwareResizeFilterConvolutionType.Prewitt), Description("Gets or sets the convolution type")]
		public ContentAwareResizeFilterConvolutionType ConvolutionType
		{
			get { return (ContentAwareResizeFilterConvolutionType)(ViewState["ConvolutionType"] ?? ContentAwareResizeFilterConvolutionType.Prewitt); }
			set { ViewState["ConvolutionType"] = value; }
		}

		#endregion

		public override void ApplyFilter(FastBitmap bitmap)
		{
			if (Width == Unit.Empty && Height == Unit.Empty)
				throw new DynamicImageException("At least one of Width or Height must be set.");

			string sourceFileName = Path.GetTempFileName();
			try
			{
				bitmap.Save(sourceFileName);

				string outputFileName = Path.GetTempFileName();

				try
				{
					int width = (Width == Unit.Empty) ? bitmap.Width : Unit.GetCalculatedValue(Width, bitmap.Width);
					int height = (Height == Unit.Empty) ? bitmap.Height : Unit.GetCalculatedValue(Height, bitmap.Height);
					new CairWrapper().ProcessImage(sourceFileName, outputFileName, int.MaxValue,
						width, height, ConvolutionType);
					FastBitmap output = new FastBitmap(File.ReadAllBytes(outputFileName));
					if (output.InnerBitmap != null)
						bitmap.InnerBitmap = output.InnerBitmap;
				}
				finally
				{
					File.Delete(outputFileName);
				}
			}
			finally
			{
				File.Delete(sourceFileName);
			}
		}
	}
}