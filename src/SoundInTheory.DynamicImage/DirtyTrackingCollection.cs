using System.Collections.Generic;
using System.Text;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Provides an abstract base class for collections of 
	/// <typeparamref name="T"/>-derived objects.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	public abstract class DirtyTrackingCollection<T> : List<T>, IDirtyTrackingObject
		where T : IDirtyTrackingObject
	{
		#region Constructors

		protected DirtyTrackingCollection()
		{
			
		}

		protected DirtyTrackingCollection(IEnumerable<T> items)
			: base(items)
		{
			
		}

		#endregion

		#region Methods

		string IDirtyTrackingObject.GetDirtyProperties()
		{
			var sb = new StringBuilder();
			sb.Append("[");
			foreach (var item in this)
				sb.Append(item.GetDirtyProperties());
			sb.Append("]");
			return sb.ToString();
		}

		#endregion
	}
}
