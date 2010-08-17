using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.IO;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Provides a base class for objects which require view state management.
	/// </summary>
	public abstract class StateManagedObject : IStateManagedObject, IStateManager
	{
		#region Fields

		private bool _isTrackingViewState;
		private StateBag _viewState;

		#endregion

		#region Properties

		bool IStateManager.IsTrackingViewState
		{
			get { return this.IsTrackingViewState; }
		}

		/// <summary>
		/// Returns a value indicating whether any properties have been defined in the state bag.
		/// </summary>
		/// <value>
		/// <c>true</c> if there are style elements defined in the state bag; otherwise, <c>false</c>.
		/// </value>
		protected virtual bool IsTrackingViewState
		{
			get { return _isTrackingViewState; }
		}

		/// <summary>
		/// <para>This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.</para>
		/// <para>Gets the state bag that holds the style elements.</para>
		/// </summary>
		/// <value>
		/// A state bag that holds the properties defined in it.
		/// </value>
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected StateBag ViewState
		{
			get
			{
				if (_viewState == null)
				{
					_viewState = new StateBag(false);
					if (_isTrackingViewState)
						((IStateManager) _viewState).TrackViewState();
				}
				return _viewState;
			}
		}

		#endregion

		#region Methods

		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		object IStateManager.SaveViewState()
		{
			return this.SaveViewState(false);
		}

		object IStateManagedObject.SaveViewState(bool saveAll)
		{
			return this.SaveViewState(saveAll);
		}

		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>
		/// Loads the previously saved state.
		/// </summary>
		/// <param name="savedState">The previously saved state.</param>
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState != null)
				((IStateManager) ViewState).LoadViewState(savedState);
		}

		/// <summary>
		/// A protected method. Saves any state that has been modified after the 
		/// <see cref="TrackViewState()" /> method was invoked.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected virtual object SaveViewState(bool saveAll)
		{
			if (_viewState != null)
				if (saveAll)
				{
					Triplet triplet = new Triplet();
					triplet.First = "Type";
					triplet.Second = this.GetType().FullName;
					triplet.Third = _viewState.SaveAllViewState();
					return triplet;
				}
				else
				{
					return ((IStateManager) _viewState).SaveViewState();
				}
			return null;
		}

		/// <summary>
		/// A protected method. Marks the beginning for tracking state changes on the object. 
		/// Any changes made after "mark" will be tracked and saved as part of the object view state.
		/// </summary>
		protected virtual void TrackViewState()
		{
			_isTrackingViewState = true;
			if (_viewState != null)
				((IStateManager) _viewState).TrackViewState();
		}

		/// <summary>
		/// Marks the <see cref="SoundInTheory.DynamicImage.StateManagedObject" /> so that its 
		/// state will be recorded in view state.
		/// </summary>
		void IStateManagedObject.SetDirty()
		{
			if (_viewState != null)
				_viewState.SetDirty(true);
		}

		/// <summary>
		/// Creates a unique key which describes the current object. This key is used
		/// by <see cref="SoundInTheory.DynamicImage.Caching.DynamicImageCacheManager" />
		/// to cache dynamically generated images.
		/// </summary>
		/// <returns>A unique key which describes the current object.</returns>
		public string GetCacheKey()
		{
			object allViewState = SaveViewState(true);
			
			LosFormatter formatter = new LosFormatter();
			StringWriter output = new StringWriter();
			formatter.Serialize(output, allViewState);

			return output.ToString();
		}

		#endregion
	}
}
