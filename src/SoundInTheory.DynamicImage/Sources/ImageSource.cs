using System.Collections.Generic;
using SoundInTheory.DynamicImage.Caching;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Sources
{
	public abstract class ImageSource : StateManagedObject
	{
		public abstract FastBitmap GetBitmap();

		public virtual void PopulateDependencies(List<Dependency> dependencies) { }
	}
}
