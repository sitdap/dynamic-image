using System.Collections.Generic;
using System.Text;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Provides a base class for objects which require view state management.
	/// </summary>
	public abstract class DirtyTrackingObject : IDirtyTrackingObject
	{
		#region Fields

		private readonly Dictionary<string, object> _propertyStore;

		#endregion

		#region Properties

		protected object this[string key]
		{
			get { return _propertyStore.ContainsKey(key) ? _propertyStore[key] : null; }
			set { _propertyStore[key] = value; }
		}

		#endregion

		#region Constructor

		protected DirtyTrackingObject()
		{
			_propertyStore = new Dictionary<string, object>();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Creates a unique key which describes the current object. This key is used
		/// by <see cref="SoundInTheory.DynamicImage.Caching.DynamicImageCacheManager" />
		/// to cache dynamically generated images.
		/// </summary>
		/// <returns>A unique key which describes the current object.</returns>
		string IDirtyTrackingObject.GetDirtyProperties()
		{
			// Loop through properties.
			var sb = new StringBuilder();
			sb.Append("{");
			sb.AppendFormat("Type: {0};", GetType().Name);
			foreach (var kvp in _propertyStore)
			{
				if (kvp.Value is IDirtyTrackingObject)
					sb.AppendFormat("{0}: {1};", kvp.Key, ((IDirtyTrackingObject)kvp.Value).GetDirtyProperties());
				else
					sb.AppendFormat("{0}: {1};", kvp.Key, kvp.Value);
			}
			sb.Append("}");
			return sb.ToString();
		}

		#endregion
	}

	public interface IDirtyTrackingObject
	{
		string GetDirtyProperties();
	}
}
