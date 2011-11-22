using System.Linq;
using System.Web.UI;

namespace SoundInTheory.DynamicImage
{
	public static class ExtensionMethods
	{
		public static object SaveAllViewState(this StateBag stateBag)
		{
			return stateBag.Keys
				.Cast<string>()
				.Select(v => new Pair(v, stateBag[v]))
				.ToArray();
		} 
	}
}