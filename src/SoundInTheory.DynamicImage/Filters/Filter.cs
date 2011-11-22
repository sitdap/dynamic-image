using System.ComponentModel;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage.Filters
{
	/// <summary>
	/// Provides the abstract base class for filters.
	/// </summary>
	public abstract class Filter : DirtyTrackingObject
	{
		#region Properties

		[Browsable(true), DefaultValue("")]
		public string Name
		{
			get
			{
				object value = this.ViewState["Name"];
				if (value != null)
					return (string) value;
				return string.Empty;
			}
			set
			{
				this.ViewState["Name"] = value;
			}
		}

		[Browsable(true), DefaultValue(true), NotifyParentProperty(true)]
		public bool Enabled
		{
			get
			{
				object value = this.ViewState["Enabled"];
				if (value != null)
					return (bool) value;
				return true;
			}
			set
			{
				this.ViewState["Enabled"] = value;
			}
		}

		#endregion

		/// <summary>
		/// When overridden in a derived class, applies the filter algorithm to
		/// the specified image.
		/// </summary>
		/// <param name="bitmap">Image to apply the 
		/// <see cref="SoundInTheory.DynamicImage.Filters.Filter" /> to.</param>
		public abstract void ApplyFilter(FastBitmap bitmap);
	}
}
