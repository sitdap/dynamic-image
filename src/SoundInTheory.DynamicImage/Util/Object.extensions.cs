using System;
using System.Reflection;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace SoundInTheory.DynamicImage.Util
{
	public static class ObjectExtensionMethods
	{
		public static void SetValue(this object parentObject, string hierarchicalPropertyName, object value, bool ignoreCase)
		{
			SetValue(parentObject, hierarchicalPropertyName, value, ".", ignoreCase);
		}

		public static void SetValue(this object parentObject, string hierarchicalPropertyName, object value, string separator, bool ignoreCase)
		{
			if (parentObject == null)
				return;

			// Get property.
			string[] propertyNames = hierarchicalPropertyName.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
			BindingFlags propertySearchBindingFlags = ((ignoreCase) ? BindingFlags.IgnoreCase : BindingFlags.Default) | BindingFlags.Instance | BindingFlags.Public;
			object currentObject = parentObject; PropertyInfo currentProperty = null;
			for (int i = 0; i < propertyNames.Length; i++)
			{
				currentProperty = currentObject.GetType().GetProperty(propertyNames[i], propertySearchBindingFlags);
				if (currentProperty == null)
					throw new Exception(string.Format("Could not find property '{0}' on object of type '{1}'", propertyNames[i], currentObject.GetType().FullName));
				if (i < propertyNames.Length - 1)
					currentObject = currentProperty.GetValue(currentObject, null);
			}

			// Convert property value if necessary.
			if (value != null && value.GetType() != currentProperty.PropertyType)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(currentObject)[currentProperty.Name];
				value = propertyDescriptor.Converter.ConvertFrom(value);
			}

			// Set property value.
			currentProperty.SetValue(currentObject, value, null);
		}
	}
}
