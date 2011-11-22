using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Provides a base class for objects which require view state management.
	/// </summary>
	public abstract class DirtyTrackingObject : IDirtyTrackingObject
	{
		#region Fields

		private Dictionary<string, object> _propertyStore;

		#endregion

		#region Properties

		protected Dictionary<string, object> PropertyStore
		{
			get
			{
				if (_propertyStore == null)
					_propertyStore = new Dictionary<string, object>();
				return _propertyStore;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Creates a unique key which describes the current object. This key is used
		/// by <see cref="SoundInTheory.DynamicImage.Caching.DynamicImageCacheManager" />
		/// to cache dynamically generated images.
		/// </summary>
		/// <returns>A unique key which describes the current object.</returns>
		public string GetDirtyProperties()
		{
			// Loop through properties.
			StringBuilder sb = new StringBuilder();
			sb.Append("{");
			foreach (var kvp in PropertyStore)
			{
				if (kvp.Value is IDirtyTrackingObject)
					sb.AppendFormat("{0}: {1};", kvp.Key, ((IDirtyTrackingObject)kvp.Value).GetDirtyProperties());
				if (kvp.Value is string)
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
