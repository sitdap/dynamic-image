using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SoundInTheory.DynamicImage
{
	public class Fill : DataBoundObject
	{
		#region Properties

		[Browsable(true), DefaultValue(FillType.Solid)]
		public FillType Type
		{
			get
			{
				object value = this.ViewState["Type"];
				if (value != null)
					return (FillType) value;
				return FillType.Solid;
			}
			set
			{
				this.ViewState["Type"] = value;
			}
		}

		[Browsable(true), DefaultValue(typeof(Colors), "White"), TypeConverter(typeof(ColorConverter))]
		public Color BackgroundColour
		{
			get
			{
				object value = this.ViewState["BackgroundColour"];
				if (value != null)
					return (Color) value;
				return Colors.White;
			}
			set
			{
				this.ViewState["BackgroundColour"] = value;
			}
		}

		[Browsable(true), DefaultValue(typeof(Colors), "White")]
		public Color GradientColourStart
		{
			get
			{
				object value = this.ViewState["GradientColourStart"];
				if (value != null)
					return (Color) value;
				return Colors.White;
			}
			set
			{
				this.ViewState["GradientColourStart"] = value;
			}
		}

		[Browsable(true), DefaultValue(typeof(Colors), "Black")]
		public Color GradientColourEnd
		{
			get
			{
				object value = this.ViewState["GradientColourEnd"];
				if (value != null)
					return (Color) value;
				return Colors.Black;
			}
			set
			{
				this.ViewState["GradientColourEnd"] = value;
			}
		}

		[Browsable(true), DefaultValue(0)]
		public int GradientAngle
		{
			get
			{
				object value = this.ViewState["GradientAngle"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.ViewState["GradientAngle"] = value;
			}
		}

		#endregion

		public Brush GetBrush()
		{
			switch (this.Type)
			{
				case FillType.Solid:
					return new SolidColorBrush(BackgroundColour);
				case FillType.Gradient:
					return new LinearGradientBrush(
						GradientColourStart, GradientColourEnd,
						GradientAngle);
				default :
					throw new NotSupportedException();
			}
		}

		public void Apply(DrawingContext dc, Rect bounds)
		{
			Brush brush = GetBrush();
			dc.DrawRectangle(brush, null, bounds);
		}
	}
}
