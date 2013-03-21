using System;
using System.Windows;
using System.Windows.Media;
using SoundInTheory.DynamicImage.Util;

namespace SoundInTheory.DynamicImage
{
	public class Fill : DirtyTrackingObject
	{
		#region Properties

		public FillType Type
		{
			get { return (FillType)(this["Type"] ?? FillType.Solid); }
			set { this["Type"] = value; }
		}

		public Color BackgroundColour
		{
			get { return (Color)(this["BackgroundColour"] ?? Colors.White); }
			set { this["BackgroundColour"] = value; }
		}

		public Color GradientColourStart
		{
			get { return (Color)(this["GradientColourStart"] ?? Colors.White); }
			set { this["GradientColourStart"] = value; }
		}

		public Color GradientColourEnd
		{
			get { return (Color)(this["GradientColourEnd"] ?? Colors.Black); }
			set { this["GradientColourEnd"] = value; }
		}

		public int GradientAngle
		{
			get { return (int)(this["GradientAngle"] ?? 0); }
			set { this["GradientAngle"] = value; }
		}

		#endregion

		public Brush GetBrush()
		{
			switch (Type)
			{
				case FillType.Solid:
					return new SolidColorBrush(BackgroundColour.ToWpfColor());
				case FillType.Gradient:
					return new LinearGradientBrush(
						GradientColourStart.ToWpfColor(), GradientColourEnd.ToWpfColor(),
						GradientAngle);
				default:
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
