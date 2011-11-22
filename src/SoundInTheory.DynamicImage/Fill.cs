using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage
{
	public class Fill : DirtyTrackingObject
	{
		#region Properties

		[Browsable(true), DefaultValue(FillType.Solid)]
		public FillType Type
		{
			get
			{
				object value = this.PropertyStore["Type"];
				if (value != null)
					return (FillType) value;
				return FillType.Solid;
			}
			set
			{
				this.PropertyStore["Type"] = value;
			}
		}

		[Browsable(true), DefaultValue(typeof(Colors), "White"), TypeConverter(typeof(ColorConverter))]
		public Color BackgroundColour
		{
			get
			{
				object value = this.PropertyStore["BackgroundColour"];
				if (value != null)
					return (Color) value;
				return Colors.White;
			}
			set
			{
				this.PropertyStore["BackgroundColour"] = value;
			}
		}

		[Browsable(true), DefaultValue(typeof(Colors), "White")]
		public Color GradientColourStart
		{
			get
			{
				object value = this.PropertyStore["GradientColourStart"];
				if (value != null)
					return (Color) value;
				return Colors.White;
			}
			set
			{
				this.PropertyStore["GradientColourStart"] = value;
			}
		}

		[Browsable(true), DefaultValue(typeof(Colors), "Black")]
		public Color GradientColourEnd
		{
			get
			{
				object value = this.PropertyStore["GradientColourEnd"];
				if (value != null)
					return (Color) value;
				return Colors.Black;
			}
			set
			{
				this.PropertyStore["GradientColourEnd"] = value;
			}
		}

		[Browsable(true), DefaultValue(0)]
		public int GradientAngle
		{
			get
			{
				object value = this.PropertyStore["GradientAngle"];
				if (value != null)
					return (int) value;
				return 0;
			}
			set
			{
				this.PropertyStore["GradientAngle"] = value;
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
