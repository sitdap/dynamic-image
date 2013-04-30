using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using SoundInTheory.DynamicImage;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof($rootnamespace$.App_Start.DynamicImage), "PreStart")]

namespace $rootnamespace$.App_Start
{
	public static class DynamicImage
	{
		public static void PreStart()
		{
			DynamicModuleUtility.RegisterModule(typeof(DynamicImageModule));
		}
	}
}