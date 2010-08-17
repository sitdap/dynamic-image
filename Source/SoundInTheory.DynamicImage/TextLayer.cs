using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public class TextLayer : Layer
	{
		private Font _font;

		#region Properties

		/// <summary>
		/// Width of the text layer; if omitted, size will be calculated automatically 
		/// and all text will be rendered on a single line.
		/// </summary>
		[Browsable(true), DefaultValue(null), Category("Layout"), Description("Width of the text layer; if omitted, size will be calculated automatically and all text will be rendered on a single line")]
		public int? Width
		{
			get
			{
				object value = this.ViewState["Width"];
				if (value != null)
					return (int?) value;
				return null;
			}
			set
			{
				this.ViewState["Width"] = value;
			}
		}

		/// <summary>
		/// Height of the text layer; if both Width and Height are omitted,
		/// size will be calculated automatically and all text will be rendered
		/// on a single line; if just Height is omitted then text will be 
		/// wrapped based on the Width.
		/// </summary>
		[Browsable(true), DefaultValue(null), Category("Layout"), Description("Height of the text layer; if both Width and Height are omitted, size will be calculated automatically and all text will be rendered on a single line; if just Height is omitted then text will be wrapped based on the Width.")]
		public int? Height
		{
			get
			{
				object value = this.ViewState["Height"];
				if (value != null)
					return (int?) value;
				return null;
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		[Browsable(true), DefaultValue(false), Category("Layout")]
		public bool Multiline
		{
			get
			{
				object value = this.ViewState["Multiline"];
				if (value != null)
					return (bool) value;
				return false;
			}
			set
			{
				this.ViewState["Multiline"] = value;
			}
		}

		[Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Font Font
		{
			get
			{
				if (_font == null)
				{
					_font = new Font();
					if (this.IsTrackingViewState)
						((IStateManager) _font).TrackViewState();
				}
				return _font;
			}
			set
			{
				if (_font != null)
					throw new Exception("You can only set a new Font if one does not already exist");

				_font = value;
				if (this.IsTrackingViewState)
					((IStateManager) _font).TrackViewState();
			}
		}

		[Browsable(true), DefaultValue(typeof(Colors), "Black")]
		public Color ForeColour
		{
			get
			{
				object value = this.ViewState["ForeColour"];
				if (value != null)
					return (Color) value;
				return Colors.Black;
			}
			set
			{
				this.ViewState["ForeColour"] = value;
			}
		}

		[Browsable(true), DefaultValue(null)]
		public Color? ClearTypeBackColour
		{
			get
			{
				object value = this.ViewState["ClearTypeBackColour"];
				if (value != null)
					return (Color?) value;
				return null;
			}
			set
			{
				this.ViewState["ClearTypeBackColour"] = value;
			}
		}

		[Browsable(true), DefaultValue(null)]
		public Color? StrokeColour
		{
			get
			{
				object value = this.ViewState["StrokeColour"];
				if (value != null)
					return (Color) value;
				return null;
			}
			set
			{
				this.ViewState["StrokeColour"] = value;
			}
		}

		[Browsable(true), DefaultValue(0)]
		public double StrokeWidth
		{
			get
			{
				object value = this.ViewState["StrokeWidth"];
				if (value != null)
					return (double)value;
				return 0;
			}
			set
			{
				this.ViewState["StrokeWidth"] = value;
			}
		}

		[Browsable(true), DefaultValue("")]
		public string Text
		{
			get
			{
				object value = this.ViewState["Text"];
				if (value != null)
					return (string) value;
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		[Browsable(true), DefaultValue(TextAlignment.Left)]
		public TextAlignment HorizontalTextAlignment
		{
			get
			{
				object value = this.ViewState["TextAlignment"];
				if (value != null)
					return (TextAlignment)value;
				return TextAlignment.Left;
			}
			set
			{
				this.ViewState["TextAlignment"] = value;
			}
		}

		public override bool HasFixedSize
		{
			get { return true; }
		}

		#endregion

		protected override void CreateImage()
		{
			// TODO: Vertical text alignment.

			// if width and height are not set, we need to measure the string
			int calculatedWidth, calculatedHeight;
			if (this.Width == null || this.Height == null)
			{
				Size measuredSize = MeasureString();

				double width = this.Width ?? measuredSize.Width;
				double height = this.Height ?? measuredSize.Height;
				calculatedWidth = (int) width;
				calculatedHeight = (int)height;
			}
			else // otherwise just create the image at the desired size
			{
				calculatedWidth = Width.Value;
				calculatedHeight = Height.Value;
			}

			#region Draw text

			DrawingVisual dv = new DrawingVisual();
			DrawingContext dc = dv.RenderOpen();

			//RenderOptions.SetClearTypeHint(dv, ClearTypeHint.Auto);
			TextOptions.SetTextRenderingMode(dv, TextRenderingMode.Aliased);
			//TextOptions.SetTextFormattingMode(dv, TextFormattingMode.Ideal)

			//Rectangle bounds = new Rectangle(0, 0, this.Bitmap.Width, this.Bitmap.Height);
			UseFormattedText(ft =>
			{
				Pen pen = null;
				if (StrokeWidth > 0)
					pen = new Pen(new SolidColorBrush(StrokeColour.Value), StrokeWidth);
				dc.DrawGeometry(new SolidColorBrush(ForeColour), pen, ft.BuildGeometry(new Point()));
			});

			dc.Close();

			RenderTargetBitmap rtb = RenderTargetBitmapUtility.CreateRenderTargetBitmap(calculatedWidth, calculatedHeight);
			rtb.Render(dv);

			#endregion

			Bitmap = new FastBitmap(rtb);
		}

		private Size MeasureString()
		{
			Size size = System.Windows.Size.Empty;
			UseFormattedText(ft =>
			{
				size = new Size(ft.WidthIncludingTrailingWhitespace, ft.Height);
			});
			return size;
		}

		private void UseFormattedText(RenderCallback renderCallback)
		{
			Brush textBrush = new SolidColorBrush(ForeColour);
			FontDescription fontDescription = Font.GetFontDescription(DesignMode);
			FormattedText formattedText = new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
			                                                fontDescription.Typeface, fontDescription.Size, textBrush);
			formattedText.SetTextDecorations(fontDescription.TextDecorations);
			if (Width != null)
				formattedText.MaxTextWidth = Width.Value;
			if (Height != null)
				formattedText.MaxTextHeight = Height.Value;
			if (!Multiline)
				formattedText.MaxLineCount = 1;
			formattedText.Trimming = TextTrimming.None;
			formattedText.TextAlignment = HorizontalTextAlignment;

			renderCallback(formattedText);
		}

		public override string ToString()
		{
			return string.Format("Text Layer: {0}", this.Text);
		}

		private delegate void RenderCallback(FormattedText formattedText);

		#region View state implementation

		/// <summary>
		/// Loads the previously saved state of the <see cref="TextLayer" /> object.
		/// </summary>
		/// <param name="savedState">
		/// An object containing the saved view state values for the <see cref="TextLayer" /> object.
		/// </param>
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Pair pair = (Pair) savedState;
				if (pair.First != null)
					base.LoadViewState(pair.First);
				if (pair.Second != null)
					((IStateManager) this.Font).LoadViewState(pair.Second);
			}
		}

		/// <summary>
		/// Saves the current view state of the <see cref="TextLayer" /> object.
		/// </summary>
		/// <param name="saveAll"><c>true</c> if all values should be saved regardless
		/// of whether they are dirty; otherwise <c>false</c>.</param>
		/// <returns>An object that represents the saved state. The default is <c>null</c>.</returns>
		protected override object SaveViewState(bool saveAll)
		{
			Pair pair = new Pair();
			pair.First = base.SaveViewState(saveAll);
			if (_font != null)
				pair.Second = ((IStateManager) _font).SaveViewState();
			return (pair.First == null && pair.Second == null) ? null : pair;
		}

		/// <summary>
		/// Tracks view state changes to the <see cref="TextLayer" /> object.
		/// </summary>
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (_font != null)
				((IStateManager) _font).TrackViewState();
		}

		#endregion
	}
}
