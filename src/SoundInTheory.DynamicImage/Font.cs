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
				object value = this.ViewState["Bold"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.ViewState["Bold"] = value;
			}
		}

		[DefaultValue(false)]
		public bool Italic
		{
			get
			{
				object value = this.ViewState["Italic"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.ViewState["Italic"] = value;
			}
		}

		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue("Verdana"), TypeConverter(typeof(FontFamilyConverter))]
		public string Name
		{
			get
			{
				object value = this.ViewState["Name"];
				if (value != null)
					return (string) value;
				return "Verdana";
			}
			set
			{
				this.ViewState["Name"] = value;
			}
		}

		/// <summary>
		/// Gets or sets the custom font file to use for this Font object. The file can be specified as a relative URL (~/assets/fonts/arial.ttf), or full rooted path pointing to the custom font file on a local disk.
		/// </summary>
		[Description("Gets or sets the custom font file to use for this Font object. The file can be specified as a relative URL (~/assets/fonts/arial.ttf), or full rooted path pointing to the custom font file on a local disk.")]
		[DefaultValue("")]
		public string CustomFontFile
		{
			get { return ViewState["CustomFontFile"] as string ?? string.Empty; }
			set { ViewState["CustomFontFile"] = value; }
		}

		[DefaultValue(18)]
		public float Size
		{
			get
			{
				object value = this.ViewState["Size"];
				if (value != null)
					return (float) value;
				return 18;
			}
			set
			{
				this.ViewState["Size"] = value;
			}
		}

		[DefaultValue(false)]
		public bool Strikeout
		{
			get
			{
				object value = this.ViewState["Strikeout"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.ViewState["Strikeout"] = value;
			}
		}

		[DefaultValue(false)]
		public bool Underline
		{
			get
			{
				object value = this.ViewState["Underline"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.ViewState["Underline"] = value;
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
