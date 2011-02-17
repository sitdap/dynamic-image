using System;
using System.ComponentModel;
using System.Reflection;
using Coolite.Ext.Web;
using SoundInTheory.DynamicImage;

namespace DemoWebsite
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ImageLayer imageLayer = new ImageLayer();

			PropertyGridParameterCollection propertyGridParameters = new PropertyGridParameterCollection();
			foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(imageLayer))
			{
				object value = propertyDescriptor.GetValue(imageLayer);

				PropertyGridParameter propertyGridParameter = new PropertyGridParameter(propertyDescriptor.DisplayName, (value != null) ? value.ToString() : "[Null]");
				if (propertyDescriptor.PropertyType.IsEnum)
				{
					ComboBox comboBox = new ComboBox { ID = "blah" + propertyDescriptor.Name, EmptyText = "Please select...", Mode = DataLoadMode.Local, TriggerAction = TriggerAction.All };
					foreach (string enumValue in (Enum.GetNames(propertyDescriptor.PropertyType)))
						comboBox.Items.Add(new ListItem(enumValue, enumValue));
					propertyGridParameter.Editor.Add(comboBox);
				}
				propertyGridParameters.Add(propertyGridParameter);
			}
			prgProperties.SetSource(propertyGridParameters);
		}
	}
}
