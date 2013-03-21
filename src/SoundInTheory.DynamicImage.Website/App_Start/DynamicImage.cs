using Microsoft.Web.Infrastructure.DynamicModuleHelper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SoundInTheory.DynamicImage.Website.App_Start.DynamicImage), "Start")]

namespace SoundInTheory.DynamicImage.Website.App_Start
{
	public static class DynamicImage
	{
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(DynamicImageModule));
		}
	}
}