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

		public Color BackgroundColor
		{
			get { return (Color)(this["BackgroundColor"] ?? Colors.White); }
			set { this["BackgroundColor"] = value; }
		}

		public Color GradientColorStart
		{
			get { return (Color)(this["GradientColorStart"] ?? Colors.White); }
			set { this["GradientColorStart"] = value; }
		}

		public Color GradientColorEnd
		{
			get { return (Color)(this["GradientColorEnd"] ?? Colors.Black); }
			set { this["GradientColorEnd"] = value; }
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
					return new SolidColorBrush(BackgroundColor.ToWpfColor());
				case FillType.Gradient:
					return new LinearGradientBrush(
						GradientColorStart.ToWpfColor(), GradientColorEnd.ToWpfColor(),
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
