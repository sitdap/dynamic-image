using System;
using System.Collections;

namespace SoundInTheory.DynamicImage.Sources
{
	public class ImageSourceCollection : CustomStateManagedCollection<ImageSource>
	{
		#region Properties

		public ImageSource SingleSource
		{
			get { return (ImageSource) ((IList) this)[0]; }
			set { ((IList) this).Insert(0, value); }
		}

		#endregion

		#region Methods

		public override int Add(ImageSource imageSource)
		{
			if (this.Count > 0)
				throw new InvalidOperationException("Only one source can be specified");
			return base.Add(imageSource);
		}

		#endregion
	}
}
