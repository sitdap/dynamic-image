using System;
using System.ComponentModel;
using System.Web.UI;
using System.Windows.Media;

namespace SoundInTheory.DynamicImage
{
	public abstract class ShapeLayer : Layer
	{
		#region Properties

		[Browsable(true), DefaultValue(null), Category("Layout"), Description("Width of the shape layer")]
		public int? Width
		{
			get { return PropertyStore["Width"] as int?; }
			set { PropertyStore["Width"] = value; }
		}

		[Browsable(true), DefaultValue(null), Category("Layout"), Description("Height of the shape layer")]
		public int? Height
		{
			get { return PropertyStore["Height"] as int?; }
			set { PropertyStore["Height"] = value; }
		}

		[Browsable(true), DefaultValue(DashStyle.Solid), NotifyParentProperty(true)]
		public DashStyle StrokeDashStyle
		{
			get { return (DashStyle)(PropertyStore["StrokeDashStyle"] ?? DashStyle.Solid); }
			set { PropertyStore["StrokeDashStyle"] = value; }
		}

		[Category("Appearance"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), NotifyParentProperty(true)]
		public Fill StrokeFill
		{
			get { return (Fill)(PropertyStore["StrokeFill"] ?? (PropertyStore["StrokeFill"] = new Fill())); }
		}

		[Browsable(true), DefaultValue(0.0f), NotifyParentProperty(true)]
		public float StrokeWidth
		{
			get { return (float) (PropertyStore["StrokeWidth"] ?? 0.0f); }
			set { PropertyStore["StrokeWidth"] = value; }
		}

		[Browsable(true), DefaultValue(0)]
		public int Roundness
		{
			get { return (int) (PropertyStore["Roundness"] ?? 0); }
			set { PropertyStore["Roundness"] = value; }
		}

		public override bool HasFixedSize
		{
			get { return (Width != null && Height != null); }
		}

		#endregion

		protected override void CreateImage()
		{
			if (this.Width != null)
				this.CalculatedWidth = this.Width.Value;

			if (this.Height != null)
				this.CalculatedHeight = this.Height.Value;
		}

		protected Pen GetPen()
		{
			Pen pen = null;
			if (StrokeWidth > 0)
				pen = new Pen(StrokeFill.GetBrush(), StrokeWidth)
				{
					DashStyle = GetDashStyle()
				};
			return pen;
		}

		private System.Windows.Media.DashStyle GetDashStyle()
		{
			switch (StrokeDashStyle)
			{
				case DashStyle.Solid :
					return DashStyles.Solid;
				case DashStyle.Dash :
					return DashStyles.Dash;
				case DashStyle.Dot :
					return DashStyles.Dot;
				case DashStyle.DashDot :
					return DashStyles.DashDot;
				case DashStyle.DashDotDot :
					return DashStyles.DashDotDot;
				default :
					throw new NotSupportedException();
			}
		}
	}
}
