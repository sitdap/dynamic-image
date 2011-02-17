using System;
using System.Web.UI;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Defines the methods that any class must implement to support view state management.
	/// </summary>
	public interface IStateManagedObject : IStateManager
	{
		/// <summary>
		/// A protected method. Saves any state that has been modified after the 
		/// <see cref="IStateManager.TrackViewState()" /> method was invoked.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		object SaveViewState(bool saveAll);

		/// <summary>
		/// Marks the <see cref="IStateManagedObject" /> so that its 
		/// state will be recorded in view state.
		/// </summary>
		void SetDirty();
	}
}
