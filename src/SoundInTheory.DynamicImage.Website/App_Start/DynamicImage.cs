using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SoundInTheory.DynamicImage.Website.App_Start.DynamicImage), "PreStart")]

namespace SoundInTheory.DynamicImage.Website.App_Start
{
	public static class DynamicImage
	{
		public static void PreStart()
		{
			DynamicModuleUtility.RegisterModule(typeof(SoundInTheory.DynamicImage.DynamicImageModule));
		}
	}
}