using System;

namespace SoundInTheory.DynamicImage
{
	internal interface IDynamicImageControl
	{
		string ID
		{
			get;
		}

		Composition Composition
		{
			get;
		}

		string TemplateName
		{
			get;
			set;
		}
	}
}
