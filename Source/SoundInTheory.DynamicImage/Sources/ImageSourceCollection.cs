using System;
using System.Web.UI;
using System.Collections;

namespace SoundInTheory.DynamicImage.Sources
{
	public class ImageSourceCollection : CustomStateManagedCollection<ImageSource>
	{
		#region Static stuff

		static ImageSourceCollection()
		{
			_knownTypes = new Type[]
			{
				typeof(BytesImageSource),
				typeof(DatabaseImageSource),
				typeof(FileImageSource),
				typeof(ImageImageSource)
			};
		}

		#endregion

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

		protected override object CreateKnownType(int index)
		{
			switch (index)
			{
				case 0:
					return new BytesImageSource();
				case 1:
					return new DatabaseImageSource();
				case 2:
					return new FileImageSource();
				case 3:
					return new ImageImageSource();
			}
			throw new ArgumentOutOfRangeException("Type index is out of bounds.");
		}

		#endregion
	}
}
