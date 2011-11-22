using System;
using System.ComponentModel;
using System.Web.UI;
using System.Drawing.Design;
using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage
{
	public class Font : DirtyTrackingObject
	{
		#region Properties

		[DefaultValue(false)]
		public bool Bold
		{
			get
			{
				object value = this.PropertyStore["Bold"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.PropertyStore["Bold"] = value;
			}
		}

		[DefaultValue(false)]
		public bool Italic
		{
			get
			{
				object value = this.PropertyStore["Italic"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.PropertyStore["Italic"] = value;
			}
		}

		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue("Verdana"), TypeConverter(typeof(FontFamilyConverter))]
		public string Name
		{
			get
			{
				object value = this.PropertyStore["Name"];
				if (value != null)
					return (string) value;
				return "Verdana";
			}
			set
			{
				this.PropertyStore["Name"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the custom font file to use for this Font object. The file can be specified as a relative URL (~/assets/fonts/arial.ttf), or full rooted path pointing to the custom font file on a local disk.
		/// </summary>
		[Description("Gets or sets the custom font file to use for this Font object. The file can be specified as a relative URL (~/assets/fonts/arial.ttf), or full rooted path pointing to the custom font file on a local disk.")]
		[DefaultValue("")]
		public string CustomFontFile
		{
			get { return PropertyStore["CustomFontFile"] as string ?? string.Empty; }
			set { PropertyStore["CustomFontFile"] = value; }
		}

		[DefaultValue(18)]
		public float Size
		{
			get
			{
				object value = this.PropertyStore["Size"];
				if (value != null)
					return (float) value;
				return 18;
			}
			set
			{
				this.PropertyStore["Size"] = value;
			}
		}

		[DefaultValue(false)]
		public bool Strikeout
		{
			get
			{
				object value = this.PropertyStore["Strikeout"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.PropertyStore["Strikeout"] = value;
			}
		}

		[DefaultValue(false)]
		public bool Underline
		{
			get
			{
				object value = this.PropertyStore["Underline"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.PropertyStore["Underline"] = value;
			}
		}

		#endregion

		public FontDescription GetFontDescription()
		{
			FontFamily fontFamily;
			if (!string.IsNullOrEmpty(CustomFontFile))
			{
				string fontFileName = Sources.FileSourceHelper.ResolveFileName(CustomFontFile);
				fontFamily = new FontFamily(fontFileName + "#" + Name);
			}
			else
			{
				fontFamily = new FontFamily(Name);
			}
			Typeface typeface = new Typeface(fontFamily, GetStyle(), GetWeight(), FontStretches.Normal);
			return new FontDescription(typeface, GetTextDecorations(), Size);
		}

		private TextDecorationCollection GetTextDecorations()
		{
			TextDecorationCollection textDecorations = new TextDecorationCollection();
			if (Strikeout)
				textDecorations.Add(TextDecorations.Strikethrough);
			if (Underline)
				textDecorations.Add(TextDecorations.Underline);
			return textDecorations;
		}

		private FontStyle GetStyle()
		{
			if (Italic)
				return FontStyles.Italic;
			return FontStyles.Normal;
		}

		private FontWeight GetWeight()
		{
			if (Bold)
				return FontWeights.Bold;
			return FontWeights.Normal;
		}
	}
}
