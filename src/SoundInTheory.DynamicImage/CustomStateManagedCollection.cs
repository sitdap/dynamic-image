using System.Collections.Generic;
using System.Text;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Provides an abstract base class for collections of 
	/// <typeparamref name="T"/>-derived objects.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	public abstract class CustomStateManagedCollection<T> : List<T>, IDirtyTrackingObject
		where T : IDirtyTrackingObject
	{
		#region Constructors

		protected CustomStateManagedCollection()
		{
			
		}

		protected CustomStateManagedCollection(IEnumerable<T> items)
			: base(items)
		{
			
		}

		#endregion

		#region Methods

		public string GetDirtyProperties()
		{
			var sb = new StringBuilder();
			sb.Append("{");
			sb.AppendFormat("Count: {0};", Count);
			foreach (var item in this)
				sb.Append(item.GetDirtyProperties());
			sb.Append("}");
			return sb.ToString();
		}

		#endregion
	}
}
