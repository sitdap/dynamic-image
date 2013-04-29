using System.ComponentModel;
using System.Web.UI;

namespace SoundInTheory.DynamicImage
{
	public class Material : DirtyTrackingObject
	{
		[DefaultValue(typeof(Colors), "Gray")]
		public Color DiffuseColor
		{
			get { return (Color) (this["DiffuseColor"] ?? Colors.Gray); }
			set { this["DiffuseColor"] = value; }
		}

		[DefaultValue(typeof(Colors), "White")]
		public Color SpecularColor
		{
			get { return (Color)(this["SpecularColor"] ?? Colors.White); }
			set { this["SpecularColor"] = value; }
		}

		[DefaultValue(""), UrlProperty]
		public string TextureFileName
		{
			get { return (string)(this["TextureFileName"] ?? string.Empty); }
			set { this["TextureFileName"] = value; }
		}

		[DefaultValue(16)]
		public int Shininess
		{
			get { return (int)(this["Shininess"] ?? 16); }
			set { this["Shininess"] = value; }
		}

		[DefaultValue(1.0f)]
		public float Transparency
		{
			get { return (float)(this["Transparency"] ?? 1.0f); }
			set { this["Transparency"] = value; }
		}
	}
}