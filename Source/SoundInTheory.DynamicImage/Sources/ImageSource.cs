using System;
using System.ComponentModel;
using System.Web.UI;
using SoundInTheory.DynamicImage.Caching;
using System.Collections.Generic;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Sources
{
	public abstract class ImageSource : DataBoundObject
	{
		public abstract FastBitmap GetBitmap(ISite site, bool designMode);

		public virtual void PopulateDependencies(List<Dependency> dependencies) { }
	}
}
