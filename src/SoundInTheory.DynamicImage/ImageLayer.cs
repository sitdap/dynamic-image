using System.Collections.Generic;
using SoundInTheory.DynamicImage.Caching;
using SoundInTheory.DynamicImage.Sources;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public class ImageLayer : Layer
	{
		#region Properties

		public ImageSource Source
		{
			get { return (ImageSource) PropertyStore["Source"]; }
			set { PropertyStore["Source"] = value; }
		}

		/// <summary>
		/// Shortcut route to Source/FileImageSource
		/// </summary>
		public string SourceFileName
		{
			set { Source = new FileImageSource { FileName = value }; }
		}

		public ImageSource AlternateSource
		{
			get { return (ImageSource)PropertyStore["AlternateSource"]; }
			set { PropertyStore["AlternateSource"] = value; }
		}

		public override bool HasFixedSize
		{
			get { return true; }
		}

		#endregion

		protected override void CreateImage()
		{
			FastBitmap sourceValue = this.Source.GetBitmap();
			if (sourceValue != null && sourceValue.InnerBitmap != null)
			{
				this.Bitmap = sourceValue;
			}
			else if (AlternateSource != null)
			{
				sourceValue = this.AlternateSource.GetBitmap();
				if (sourceValue != null && sourceValue.InnerBitmap != null)
					this.Bitmap = sourceValue;
			}
		}

		public override void PopulateDependencies(List<Dependency> dependencies)
		{
			base.PopulateDependencies(dependencies);
			this.Source.PopulateDependencies(dependencies);
			if (this.AlternateSource != null)
				this.AlternateSource.PopulateDependencies(dependencies);
		}

		public override string ToString()
		{
			return "Image Layer";
		}
	}
}
