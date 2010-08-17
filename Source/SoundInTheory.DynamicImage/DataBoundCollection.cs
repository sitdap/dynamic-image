using System;
using System.ComponentModel;
using System.Web.UI;

namespace SoundInTheory.DynamicImage
{
	public abstract class DataBoundCollection<T> : CustomStateManagedCollection<T>
		where T : DataBoundObject
	{
		internal Control BindingContainer
		{
			set
			{
				foreach (T item in this)
					item.BindingContainer = value;
			}
		}

		internal void DataBind()
		{
			foreach (T item in this)
				item.DataBind();
		}
	}
}
