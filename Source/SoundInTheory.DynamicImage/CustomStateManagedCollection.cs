using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Collections;

namespace SoundInTheory.DynamicImage
{
	/// <summary>
	/// Provides an abstract base class for collections of 
	/// <typeparamref name="T"/>-derived objects.
	/// </summary>
	/// <typeparam name="T">The type of elements in the collection.</typeparam>
	public abstract class CustomStateManagedCollection<T> : StateManagedCollection, IStateManagedObject
		where T : DataBoundObject
	{
		protected static Type[] _knownTypes;

		#region Properties

		/// <summary>
		/// Gets or sets the object at the specified index in the collection.
		/// </summary>
		/// <param name="index">The index of the object to retrieve from the collection.</param>
		/// <value>The object at the specified index in the collection.</value>
		public T this[int index]
		{
			get { return (T) ((IList) this)[index]; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Appends the specified object to the end of the collection.
		/// </summary>
		/// <param name="value">The <typeparamref name="T"/> to append to the collection.</param>
		/// <returns>The index value of the added item.</returns>
		public virtual int Add(T value)
		{
			return ((IList) this).Add(value);
		}

		/// <summary>
		/// Gets an array of types that the <see cref="CustomStateManagedCollection{T}" /> 
		/// collection can contain.
		/// </summary>
		/// <returns>
		/// An ordered array of <see cref="System.Type" /> objects that identify the types of 
		/// objects that the collection can contain.
		/// </returns>
		protected override Type[] GetKnownTypes()
		{
			return _knownTypes;
		}

		/// <summary>
		/// Inserts the specified object into the <see cref="CustomStateManagedCollection{T}" /> 
		/// collection at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which the object is inserted.</param>
		/// <param name="value">The object to insert.</param>
		public void Insert(int index, T value)
		{
			((IList) this).Insert(index, value);
		}

		/// <summary>
		/// Performs additional custom processes when validating a value.
		/// </summary>
		/// <param name="o">The <c>object</c> being validated.</param>
		/// <remarks>
		/// The <c>OnValidate</c> method determines whether the object specified 
		/// in the <paramref name="o"/> parameter is a <typeparamref name="T"/> instance. 
		/// If not, it throws an <see cref="ArgumentException" /> exception.
		/// </remarks>
		protected override void OnValidate(object o)
		{
			base.OnValidate(o);
			if (!(o is T))
				throw new ArgumentException("Object is not of the correct type.");
		}

		/// <summary>
		/// Removes the specified object from the
		/// <see cref="CustomStateManagedCollection{T}" /> object.
		/// </summary>
		/// <param name="value">The object to remove from the collection.</param>
		public void Remove(T value)
		{
			((IList) this).Remove(value);
		}

		/// <summary>
		/// Removes the object at the specified index location from the
		/// <see cref="CustomStateManagedCollection{T}" /> object.
		/// </summary>
		/// <param name="index">The zero-based index location of the object to remove.</param>
		public void RemoveAt(int index)
		{
			((IList) this).RemoveAt(index);
		}

		/// <summary>
		/// Marks the specified object as having changed since the last 
		/// load or save from view state.
		/// </summary>
		/// <param name="o">The object to mark as having changed since the last 
		/// load or save from view state.</param>
		protected override void SetDirtyObject(object o)
		{
			((IStateManagedObject) o).SetDirty();
		}

		object IStateManagedObject.SaveViewState(bool saveAll)
		{
			if (saveAll)
			{
				Triplet triplet = new Triplet();
				triplet.First = "Count";
				triplet.Second = this.Count;
				ArrayList itemList = new ArrayList();
				foreach (StateManagedObject item in this)
					itemList.Add(((IStateManagedObject) item).SaveViewState(saveAll));
				triplet.Third = itemList;
				return triplet;
			}
			else
			{
				return ((IStateManager) this).SaveViewState();
			}
		}

		/*internal string GetCacheKey()
		{
			string cacheKey = string.Format("C:{0};", this.Count);
			for (int i = 0, length = this.Count; i < length; ++i)
				cacheKey += string.Format("L{0}:{{{1}}};", i, this[i].GetCacheKey());
			return cacheKey;
		}*/

		/*internal Control BindingContainer
		{
			set
			{
				foreach (Layer layer in this)
					layer.BindingContainer = value;
			}
		}*/

		/*internal void DataBind()
		{
			foreach (Layer layer in this)
				layer.DataBind();
		}*/

		#endregion
	}
}
