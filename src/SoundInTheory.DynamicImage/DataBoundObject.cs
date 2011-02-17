using System;
using System.ComponentModel;
using System.Web.UI;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Provides a base class for objects which require support for data binding.
	/// </summary>
	public abstract class DataBoundObject : StateManagedObject
	{
		protected virtual internal ISite Site { get; set; }
		protected virtual internal bool DesignMode { get; set; }

		/// <summary>
		/// Occurs when the server control binds to a data source.
		/// </summary>
		[Description("Occurs when the server control binds to a data source."), Category("Data")]
		public event EventHandler DataBinding;

		/// <summary>
		/// Gets the control that contains this control's data binding.
		/// </summary>
		/// <value>
		/// The <see cref="System.Web.UI.Control" /> that contains this control's data binding.
		/// </value>
		[Browsable(false), Bindable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Control BindingContainer
		{
			get;
			internal set;
		}

		/// <summary>
		/// Binds a data source to the invoked object and all its child controls.
		/// </summary>
		public virtual void DataBind()
		{
			if (DataBinding != null)
				DataBinding(this, EventArgs.Empty);
		}
	}
}
